using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TimeManagerWebAPI.Dtos.Auth;

namespace TimeManagerWebAPI.Validators.Auth
{
    public class LoginUserInputValidator : AbstractValidator<LoginUserInput>
    {
        public LoginUserInputValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage(ErrorMessages.UsernameEmpty);

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordEmpty);
        }
    }
}
