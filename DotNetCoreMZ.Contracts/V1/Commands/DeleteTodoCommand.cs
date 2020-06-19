using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreMZ.Contracts.V1.Commands
{
    public class DeleteTodoCommand : IRequest<bool>
    {
        public Guid TodoId { get; set; }
    }
}
