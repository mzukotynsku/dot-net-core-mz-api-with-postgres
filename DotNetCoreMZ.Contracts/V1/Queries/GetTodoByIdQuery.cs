using DotNetCoreMZ.API.Contracts.V1.Responses;
using MediatR;
using System;

namespace DotNetCoreMZ.API.Contracts.V1.Queries
{
    public class GetTodoByIdQuery : IRequest<TodoResponse>
    {
        public Guid TodoId { get; set; }

        public GetTodoByIdQuery(Guid todoId)
        {
            TodoId = todoId;
        }
    }
}
