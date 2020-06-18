using AutoMapper;
using DotNetCoreMZ.API.Data;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.Data.DTO;
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
        private readonly IMapper _mapper;

        public TodoService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteTodoAsync(Guid todoId)
        {
            var todo = await GetTodoByIdAsync(todoId);
            if (todo == null)
                return false;
            
            var todoDTO = _mapper.Map<TodoDTO>(todo);

            _dataContext.Todos.Remove(todoDTO);
            var deleted =await _dataContext.SaveChangesAsync();
            return deleted > 0;
        }

        public async Task<bool> CreateTodoAsync(Todo todo)
        {
            var todoDTO = _mapper.Map<TodoDTO>(todo);

            await _dataContext.Todos.AddAsync(todoDTO);
            var created = await _dataContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Todo> GetTodoByIdAsync(Guid todoId)
        {
            return _mapper.Map<Todo>(await _dataContext.Todos.SingleOrDefaultAsync(x => x.Id == todoId));
        }

        public async Task<List<Todo>> GetTodosAsync()
        {
            return _mapper.Map<List<Todo>>(await _dataContext.Todos.ToListAsync());
        }

        public async Task<bool> UpdateTodoAsync(Todo todoToUpdate)
        {
            var todoDTO = _mapper.Map<TodoDTO>(todoToUpdate);

            _dataContext.Todos.Update(todoDTO);
            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UserOwnsTodoAsync(Guid todoId, string userId)
        {
            var todo =  _mapper.Map<Todo>(await _dataContext.Todos.AsNoTracking().SingleOrDefaultAsync(x => x.Id == todoId));

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
