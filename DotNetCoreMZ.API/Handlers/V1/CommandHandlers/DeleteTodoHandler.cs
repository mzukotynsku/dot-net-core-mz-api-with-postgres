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
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, bool>
    {
        private readonly ITodoService _todoService;

        public DeleteTodoHandler(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _todoService.DeleteTodoAsync(request.TodoId);
            if (deleted)
                return true;

            return false;
        }
    }
}
