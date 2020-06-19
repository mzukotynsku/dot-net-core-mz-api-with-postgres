using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1.Queries;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Handlers.V1.QueryHandlers
{
    public class GetTodoByIdHandler : IRequestHandler<GetTodoByIdQuery, TodoResponse>
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public GetTodoByIdHandler(IMapper mapper, ITodoService todoService)
        {
            _mapper = mapper;
            _todoService = todoService;
        }

        public async Task<TodoResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todo = await _todoService.GetTodoByIdAsync(request.TodoId);
           
            return todo == null ? null : _mapper.Map<TodoResponse>(todo);
        }
    }
}
