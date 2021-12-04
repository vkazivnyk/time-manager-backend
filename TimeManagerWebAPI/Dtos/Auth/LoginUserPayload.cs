using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI.Dtos.Auth
{
    public record LoginUserPayload(string Token, string Username, string Email);
}
