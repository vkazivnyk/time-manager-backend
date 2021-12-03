using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskPutPayloadType : ObjectType<UserTaskPutPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTaskPutPayload> descriptor)
        {
            descriptor.Description("Represents the payload with the result of mutating a user task.");
        }
    }
}
