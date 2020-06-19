using DotNetCoreMZ.API.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Services
{
    public interface IUriService
    {
        Uri GetTodoUri(string todoId);
    }

    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetTodoUri(string todoId)
        {
            return new Uri(_baseUri + ApiRoutes.Todos.GetById.Replace("todoId", todoId));
        }
    }
}
