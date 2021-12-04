using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Types;

namespace TimeManagerWebAPI.GraphQL.Tasks
{
    public class UserTaskAddPayloadType : ObjectType<UserTaskAddPayload>
    {
        protected override void Configure(IObjectTypeDescriptor<UserTaskAddPayload> descriptor)
        {
            descriptor.Description("Represents the payload with the result of adding a user task");
        }
    }
}
