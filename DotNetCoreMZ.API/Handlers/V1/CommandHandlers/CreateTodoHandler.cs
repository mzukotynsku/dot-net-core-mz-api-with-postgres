using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1.Commands;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.API.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Handlers.V1.CommandHandlers
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoResponse>
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public CreateTodoHandler(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        public async Task<TodoResponse> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo
            {
                Id = request.TodoId,
                Name = request.Name,
                UserId = request.UserId
            };

            await _todoService.CreateTodoAsync(todo);

            return  _mapper.Map<TodoResponse>(todo);
        }
    }
}
