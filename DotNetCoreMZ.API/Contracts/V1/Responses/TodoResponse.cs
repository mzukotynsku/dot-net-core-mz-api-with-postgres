using System;

namespace DotNetCoreMZ.API.Contracts.V1.Responses
{
    public class TodoResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
