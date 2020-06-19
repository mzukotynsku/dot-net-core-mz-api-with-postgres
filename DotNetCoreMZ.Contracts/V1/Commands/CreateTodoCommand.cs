using DotNetCoreMZ.API.Contracts.V1.Responses;
using MediatR;
using System;

namespace DotNetCoreMZ.API.Contracts.V1.Commands
{
    public class CreateTodoCommand : IRequest<TodoResponse>
    {
        public Guid TodoId { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
    }
}
