using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskDeleteInputType : InputObjectType<UserTaskDeleteInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserTaskDeleteInput> descriptor)
        {
            descriptor.Description("Represents the input for deleting a user task.");
        }
    }
}
