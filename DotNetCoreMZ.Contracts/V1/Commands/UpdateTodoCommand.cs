using DotNetCoreMZ.API.Contracts.V1.Responses;
using MediatR;
using System;

namespace DotNetCoreMZ.Contracts.V1.Commands
{
    public class UpdateTodoCommand : IRequest<bool>
    {
        public Guid TodoId { get; set; }
        
        public string Name { get; set; }
    }
}
