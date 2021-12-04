using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TimeManageData.Models;
using TimeManagerServices.Auth;
using TimeManagerWebAPI.Dtos.Auth;

namespace TimeManagerWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        private readonly JwtTokenCreator _tokenCreator;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IHttpContextAccessor contextAccessor,
            JwtTokenCreator tokenCreator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _tokenCreator = tokenCreator;
        }
        
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserInput input)
        {
            ApplicationUser newUser = new()
            {
                UserName = input.Username,
                Email = input.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, input.Password).ConfigureAwait(false);

            await _signInManager.PasswordSignInAsync(newUser, input.Password, false, false).ConfigureAwait(false);

            var (authToken, _) = _tokenCreator.CreateAuthToken(newUser);

            LoginUserPayload payload = new (authToken, newUser.UserName, newUser.Email);

            return Ok(payload);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserInput input)
        {
            string username = input.Username;

            ApplicationUser user = await _userManager.FindByNameAsync(username);

            await _signInManager.PasswordSignInAsync(user, input.Password, false, false).ConfigureAwait(false);

            var (authToken, _) = _tokenCreator.CreateAuthToken(user);

            LoginUserPayload payload = new(authToken, user.UserName, user.Email);

            return Ok(payload);
        }

        [Authorize(Policy = "Auth")]
        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync().ConfigureAwait(false);
            }
            catch
            {
                return StatusCode(500);
            }

            return NoContent();
        }
    }
}
