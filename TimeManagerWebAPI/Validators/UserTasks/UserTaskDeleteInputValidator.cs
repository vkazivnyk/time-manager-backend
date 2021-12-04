using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.Validators
{
    public class UserTaskDeleteInputValidator : AbstractValidator<UserTaskDeleteInput>
    {
        public UserTaskDeleteInputValidator()
        {
            RuleFor(input => input.Id)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskIdEmpty);
        }
    }
}
