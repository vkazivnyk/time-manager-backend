using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;

namespace TimeManagerWebAPI.GraphQL.ErrorFilters
{
    public class GraphQLErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception is not null and not GraphQLException ?
                error.WithMessage("There was an error while processing the request.") :
                error;
        }
    }
}
