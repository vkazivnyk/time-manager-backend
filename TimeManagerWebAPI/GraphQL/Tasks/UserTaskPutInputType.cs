using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskPutInputType : InputObjectType<UserTaskPutInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserTaskPutInput> descriptor)
        {
            descriptor.Description("Represents the input for mutating a user task.");
        }
    }
}
