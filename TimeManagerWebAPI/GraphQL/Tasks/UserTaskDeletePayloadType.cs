using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskDeletePayloadType : ObjectType<UserTaskDeletePayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTaskDeletePayload> descriptor)
        {
            descriptor.Description("Represents the payload with the result of deleting a user task.");
        }
    }
}
