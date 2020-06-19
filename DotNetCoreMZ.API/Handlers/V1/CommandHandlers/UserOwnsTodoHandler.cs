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
    public class UserOwnsTodoHandler : IRequestHandler<UserOwnsTodoCommand, bool>
    {
        private readonly ITodoService _todoService;

        public UserOwnsTodoHandler(ITodoService todoService)
        {
            _todoService = todoService;
        }

        public async Task<bool> Handle(UserOwnsTodoCommand request, CancellationToken cancellationToken)
        {
            var userOwnsTodo = await _todoService.UserOwnsTodoAsync(request.TodoId, request.UserId);

            if (userOwnsTodo)
                return true;

            return false;
        }
    }
}
