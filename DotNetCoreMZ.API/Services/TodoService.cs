using DotNetCoreMZ.API.Data;
using DotNetCoreMZ.API.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Services
{
    public class TodoService : ITodoService
    {
        private readonly DataContext _dataContext;

        public TodoService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> DeleteTodoAsync(Guid todoId)
        {
            var todo = await GetTodoByIdAsync(todoId);
            if (todo == null)
                return false;

            _dataContext.Todos.Remove(todo);
            var deleted =await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> CreateTodoAsync(Todo todo)
        {
            await _dataContext.Todos.AddAsync(todo);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Todo> GetTodoByIdAsync(Guid todoId)
        {
            return await _dataContext.Todos.SingleOrDefaultAsync(x => x.Id == todoId);
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _dataContext.Todos.ToListAsync();
        }

        public async Task<bool> UpdateTodoAsync(Todo todoToUpdate)
        {
            _dataContext.Todos.Update(todoToUpdate);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UserOwnsTodoAsync(Guid todoId, string userId)
        {
            var todo =  await _dataContext.Todos.AsNoTracking().SingleOrDefaultAsync(x => x.Id == todoId);

            if(todo == null)
            {
                return false;
            }
            
            if(todo.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
