using DotNetCoreMZ.API.Contracts.V1.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Contracts.V1.Queries
{
    public class GetAllTodosQuery : IRequest<List<TodoResponse>>
    {
    }
}
