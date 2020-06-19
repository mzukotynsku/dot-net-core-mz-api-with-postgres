using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1.Commands;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Services;
using DotNetCoreMZ.Contracts.V1.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Handlers.V1.CommandHandlers
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, bool>
    {
        private readonly ITodoService _todoService;

        public UpdateTodoHandler(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoService.GetTodoByIdAsync(request.TodoId);
            todo.Name = request.Name;

            var updated = await _todoService.UpdateTodoAsync(todo);
            if (updated)
                return true;

            return false;
        }
    }
}
