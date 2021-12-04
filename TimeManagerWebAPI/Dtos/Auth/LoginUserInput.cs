using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI.Dtos.Auth
{
    public class LoginUserInput
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
