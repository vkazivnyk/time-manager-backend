using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagerWebAPI
{
    public static class ErrorMessages
    {
        public const string UserTaskIdEmpty = "User task id can not be empty.";

        public const string UserTaskNameEmpty = "User task name can not be empty.";

        public const string UserTaskDeadlineEmpty = "User task deadline can not be empty.";

        public const string CantUpdateUserTask = "There was a problem updating a user task.";

        public const string CantDeleteUserTask = "There was a problem deleting a user task.";

        public const string UsernameEmpty = "The username can not be empty.";

        public const string EmailNotValid = "The email is not valid.";

        public const string PasswordEmpty = "The password can not be empty";

        public const string PasswordLength = "The password length should be at least 6 characters";

        public const string PasswordDigit = "The password must have at least one digit.";

        public const string PasswordsDoNotMatch = "The passwords do not match";

        public const string CredentialsNotValid = "The provided credentials are not valid.";

        public const string UserExists = "A user with the provided credentials already exists.";
    }
}
