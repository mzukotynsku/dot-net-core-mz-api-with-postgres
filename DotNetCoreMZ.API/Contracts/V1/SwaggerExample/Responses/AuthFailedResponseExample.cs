using DotNetCoreMZ.API.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Configuration;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Responses
{
    public class AuthFailedResponseExample : IExamplesProvider<AuthFailedResponse>
    {
        public AuthFailedResponse GetExamples()
        {
            return new AuthFailedResponse
            {
                Errors = new List<string> { "first problem with authentication", "second problem with authentication" }
            };
        }
    }
}
