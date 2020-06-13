using DotNetCoreMZ.API.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Responses
{
    public class TodoResponseExample : IExamplesProvider<TodoResponse>
    {
        public TodoResponse GetExamples()
        {
            return new TodoResponse
            {
                Id = Guid.NewGuid(),
                Name = "new name",
                UserId = "944C81B0-B33A-45B4-8CA5-84FAD8A0B7DF"
            };
        }
    }
}
