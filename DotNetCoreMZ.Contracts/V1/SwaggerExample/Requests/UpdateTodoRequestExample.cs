using DotNetCoreMZ.API.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Requests
{
    public class UpdateTodoRequestExample : IExamplesProvider<UpdateTodoRequest>
    {
        public UpdateTodoRequest GetExamples()
        {
            return new UpdateTodoRequest
            {
                Name = "name to update"
            };
        }
    }
}
