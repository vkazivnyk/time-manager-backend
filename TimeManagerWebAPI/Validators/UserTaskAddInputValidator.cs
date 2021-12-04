using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TimeManagerWebAPI.GraphQL.Tasks;

namespace TimeManagerWebAPI.Validators
{
    public class UserTaskAddInputValidator : AbstractValidator<UserTaskAddInput>
    {
        public UserTaskAddInputValidator()
        {
            RuleFor(input => input.Name)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskNameEmpty);

            RuleFor(input => input.Deadline)
                .NotEmpty()
                .WithMessage(ErrorMessages.UserTaskDeadlineEmpty);
        }
    }
}
