using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreMZ.Contracts.V1.Commands
{
    public class UserOwnsTodoCommand : IRequest<bool>
    {
        public Guid TodoId { get; set; }

        public string UserId { get; set; }
    }
}
