using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using TimeManagerWebAPI.Dtos.Auth;

namespace TimeManagerWebAPI.Validators.Auth
{
    public class RegisterUserInputValidator : AbstractValidator<RegisterUserInput>
    {
        public RegisterUserInputValidator()
        {
            RuleFor(u => u.Username)
                .NotEmpty()
                .WithMessage(ErrorMessages.UsernameEmpty);

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage(ErrorMessages.EmailNotValid);

            RuleFor(u => u.Password)
                .NotEmpty()
                .WithMessage(ErrorMessages.PasswordEmpty);

            RuleFor(u => u.Password)
                .MinimumLength(6)
                .WithMessage(ErrorMessages.PasswordLength);
                
            RuleFor(u => u.Password)
                .Matches(@"\d")
                .WithMessage(ErrorMessages.PasswordDigit);

            RuleFor(u => u.PasswordConfirm)
                .Equal(u => u.Password)
                .WithMessage(ErrorMessages.PasswordsDoNotMatch);
        }
    }
}
