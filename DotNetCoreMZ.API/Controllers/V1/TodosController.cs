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
    [Produces("application/json")]
    public class TodosController : Controller
    {
        private readonly ITodoService _todoService;
        private readonly IMapper _mapper;

        public TodosController(ITodoService todoService, IMapper mapper)
        {
            _todoService = todoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the todos
        /// </summary>
        /// <response code="200">Returns all the todos</response>
        [AllowAnonymous] // fro testing purspose, change it later
        [ProducesResponseType(typeof(List<TodoResponse>),200)]
        [HttpGet(ApiRoutes.Todos.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _todoService.GetTodosAsync();

            return Ok(_mapper.Map<List<TodoResponse>>(todos));
        }

        /// <summary>
        /// Returns specific todo by id 
        /// </summary>
        /// <response code="200">Returns specific todo</response>
        /// <response code="404">Not found the todo to display</response>
        [ProducesResponseType(typeof(TodoResponse),200)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpGet(ApiRoutes.Todos.GetById)]
        public async Task<IActionResult> GetById([FromRoute] Guid todoId)
        {
            var todo = await _todoService.GetTodoByIdAsync(todoId);

            if (todo == null)
                return NotFound();

            return Ok(_mapper.Map<TodoResponse>(todo));
        }

        /// <summary>
        /// Update a todo 
        /// </summary>
        /// <response code="204">Update a todo</response>
        /// <response code="404">Not found the todo to update</response>
        [ProducesResponseType(typeof(ErrorResponse),404)]
        [HttpPut(ApiRoutes.Todos.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid todoId, [FromBody] UpdateTodoRequest todoRequest)
        {
            var userOwnsTodo = await _todoService.UserOwnsTodoAsync(todoId, HttpContext.GetUserId());

            if(!userOwnsTodo)
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not own this todo " } } });

            }

            var todo = await _todoService.GetTodoByIdAsync(todoId);
            todo.Name = todoRequest.Name;

            var updated = await _todoService.UpdateTodoAsync(todo);
            if (updated)
                return NoContent();

            return NotFound();
        }

        /// <summary>
        /// Delete a todo 
        /// </summary>
        /// <response code="204">Delete a todo</response>
        /// <response code="404">Not found the todo to delete</response>
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [HttpDelete(ApiRoutes.Todos.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid todoId)
        {
            var userOwnsTodo = await _todoService.UserOwnsTodoAsync(todoId, HttpContext.GetUserId());

            if (!userOwnsTodo)
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not own this todo " } } });
            }

            var deleted = await _todoService.DeleteTodoAsync(todoId);

            if (deleted)
                return NoContent();

            return NotFound();
        }

        /// <summary>
        /// Create a todo 
        /// </summary>
        /// <response code="201">Create a todo</response>
        [HttpPost(ApiRoutes.Todos.Create)]
        [ProducesResponseType(typeof(TodoResponse),201)]
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
