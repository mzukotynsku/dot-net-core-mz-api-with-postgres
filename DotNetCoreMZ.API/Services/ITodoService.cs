using DotNetCoreMZ.API.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync();

        Task<Todo> GetTodoByIdAsync(Guid todoId);

        Task<bool> UpdateTodoAsync(Todo todoToUpdate);

        Task<bool> DeleteTodoAsync(Guid todoId);

        Task<bool> CreateTodoAsync(Todo todo);
        
        Task<bool> UserOwnsTodoAsync(Guid todoId, string getUserId);
    }
}
