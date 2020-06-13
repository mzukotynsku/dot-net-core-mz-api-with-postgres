using DotNetCoreMZ.API.Contracts.V1.Responses;
using Swashbuckle.AspNetCore.Filters;

namespace DotNetCoreMZ.API.Contracts.V1.SwaggerExample.Responses
{
    public class AuthSuccessResponseExample : IExamplesProvider<AuthSuccessResponse>
    {
        public AuthSuccessResponse GetExamples()
        {
            return new AuthSuccessResponse
            {
                Token = "example_of_Token_1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ1c2VyQGV4YW1wbGUuY29tIiwianRpIjoiZjMxMzZkMzEtYjk0OC00YjRjLWFkZTAtZjJjZWMxNTU3ODQyIiwiZW1haWwiOiJ1c2VyQGV4YW1wbGUuY29tIiwiaWQiOiJhNWQyMjYxMy1iNzJkLTQxNjUtODJlYS1hOWVhZWNlNTM2NzIiLCJuYmYiOjE1OTE5ODk5NzYsImV4cCI6MTU5MTk5MDAyMSwiaWF0IjoxNTkxOTg5OTc2fQ.dRxIE-DhrU1JwCbw5DRmFgZx-2zwXPEH9Pqs0FaY4RU",
                RefreshToken = "example_of_Refreshtoken-86C1-4638-AB24-7C954990ACF0"
            };
        }
    }
}
