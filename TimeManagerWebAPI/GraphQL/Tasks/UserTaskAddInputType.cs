using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskAddInputType : InputObjectType<UserTaskAddInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserTaskAddInput> descriptor)
        {
            descriptor.Description("Represents the input for adding a user task");
        }
    }
}
