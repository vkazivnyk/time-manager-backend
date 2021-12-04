using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.Validators
{
    public class UserTaskPutInputValidator : AbstractValidator<UserTaskPutInput>
    {
        public UserTaskPutInputValidator()
        {
            RuleFor(input => input.Id)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskIdEmpty);

            RuleFor(input => input.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskNameEmpty);

            RuleFor(input => input.Deadline)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskDeadlineEmpty);
        }
    }
}
