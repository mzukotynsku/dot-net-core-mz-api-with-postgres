using DotNetCoreMZ.API.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Requests
{
    public class CreateTodoRequestExample : IExamplesProvider<CreateTodoRequest>
    {
        public CreateTodoRequest GetExamples()
        {
            return new CreateTodoRequest
            {
                Name = "new name",
            };
        }
    }
}
