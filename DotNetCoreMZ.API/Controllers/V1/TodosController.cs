﻿using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1;
using DotNetCoreMZ.API.Contracts.V1.Requests;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.API.Extensions;
using DotNetCoreMZ.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreMZ.API.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TodosController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodosController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Todos.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetTodosAsync();

            return Ok(_mapper.Map<List<TodoResponse>>(todos));
        }

        [HttpGet(ApiRoutes.Todos.GetById)]
        public async Task<IActionResult> GetById([FromRoute] Guid todoId)
        {
            var todo = await _todoService.GetTodoByIdAsync(todoId);

            if (todo == null)
                return NotFound();

            return Ok(_mapper.Map<TodoResponse>(todo));
        }

        [HttpPut(ApiRoutes.Todos.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid todoId, [FromBody] UpdateTodoRequest todoRequest)
        {
            var userOwnsTodo = await _todoService.UserOwnsTodoAsync(todoId, HttpContext.GetUserId());

            if(!userOwnsTodo)
            {
                return BadRequest(new { error = "You do not own this todo " });

            }

            var todo = await _todoService.GetTodoByIdAsync(todoId);
            todo.Name = todoRequest.Name;

            var updated = await _todoService.UpdateTodoAsync(todo);
            if (updated)
                return NoContent();

            return NotFound();
        }

        [HttpDelete(ApiRoutes.Todos.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid todoId)
        {
            var userOwnsTodo = await _todoService.UserOwnsTodoAsync(todoId, HttpContext.GetUserId());

            if (!userOwnsTodo)
            {
                return BadRequest(new { error = "You do not own this todo " });

            }

            var deleted = await _todoService.DeleteTodoAsync(todoId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Todos.Create)]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest todoRequest)
        {

            var todo = new Todo
            {
                Name = todoRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            await _todoService.CreateTodoAsync(todo);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Todos.GetById.Replace("{todoId}", todo.Id.ToString());

            var response = _mapper.Map<TodoResponse>(todo);

            return Created(locationUrl, response);
        }
    }
}
