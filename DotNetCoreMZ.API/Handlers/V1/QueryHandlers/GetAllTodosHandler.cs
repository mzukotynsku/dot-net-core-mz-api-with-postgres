using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1.Queries;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Handlers.V1.QueryHandlers
{
    public class GetAllTodosHandler : IRequestHandler<GetAllTodosQuery, List<TodoResponse>>
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public GetAllTodosHandler(IMapper mapper, ITodoService todoService)
        {
            _mapper = mapper;
            _todoService = todoService;
        }

        public async Task<List<TodoResponse>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoService.GetTodosAsync();

            return _mapper.Map<List<TodoResponse>>(todos);
        }
    }
}
