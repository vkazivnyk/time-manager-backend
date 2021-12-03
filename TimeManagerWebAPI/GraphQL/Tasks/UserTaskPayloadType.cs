using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskPayloadType : ObjectType<UserTaskPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTaskPayload> descriptor)
        {
            descriptor.Description("Represents the payload with a user task.");
        }
    }
}
