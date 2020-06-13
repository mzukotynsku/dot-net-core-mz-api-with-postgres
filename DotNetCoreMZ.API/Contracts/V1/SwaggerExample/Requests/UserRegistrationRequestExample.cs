using DotNetCoreMZ.API.Contracts.V1.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Requests
{
    public class UserRegistrationRequestExample : IExamplesProvider<UserRegistrationRequest>
    {
        public UserRegistrationRequest GetExamples()
        {
            return new UserRegistrationRequest
            {
                Email = "user@example.com",
                Password = "My$ecretPa$$w0rd!"
            };
        }
    }
}
