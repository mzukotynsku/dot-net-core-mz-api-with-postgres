using AutoMapper;
using DotNetCoreMZ.API.Contracts.V1;
using DotNetCoreMZ.API.Contracts.V1.Commands;
using DotNetCoreMZ.API.Contracts.V1.Queries;
using DotNetCoreMZ.API.Contracts.V1.Requests;
using DotNetCoreMZ.API.Contracts.V1.Responses;
using DotNetCoreMZ.API.Domain;
using DotNetCoreMZ.API.Extensions;
using DotNetCoreMZ.API.Services;
using DotNetCoreMZ.Contracts.V1.Commands;
using MediatR;
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
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
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
            var query = new GetAllTodosQuery();

            var result = await _mediator.Send(query);

            return Ok(result);        
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
            var query = new GetTodoByIdQuery(todoId);

            var result = await _mediator.Send(query);

            return result != null ? (IActionResult) Ok(result) : NotFound();
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
            if (!await CheckIfUserOwnsTodo(todoId))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not own this todo " } } });
            }

            var command = new UpdateTodoCommand
            {
                TodoId = todoId,
                Name = todoRequest.Name,
            };

            var result = await _mediator.Send(command);

            return result ? (IActionResult)NoContent() : NotFound();
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
            if (!await CheckIfUserOwnsTodo(todoId))
            {
                return BadRequest(new ErrorResponse { Errors = new List<ErrorModel> { new ErrorModel { Message = "You do not own this todo " } } });
            }

            var command = new DeleteTodoCommand { TodoId = todoId };

            var result = await _mediator.Send(command);

            return result ? (IActionResult)NoContent() : NotFound();
        }

        /// <summary>
        /// Create a todo 
        /// </summary>
        /// <response code="201">Create a todo</response>
        [HttpPost(ApiRoutes.Todos.Create)]
        [ProducesResponseType(typeof(TodoResponse),201)]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequest todoRequest)
        {
            var command = new CreateTodoCommand
            {
                TodoId = Guid.NewGuid(),
                Name = todoRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            var result = await _mediator.Send(command);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUrl = baseUrl + "/" + ApiRoutes.Todos.GetById.Replace("{todoId}", result.Id.ToString());

            return Created(locationUrl, result);
        }

        private async Task<bool> CheckIfUserOwnsTodo(Guid todoId)
        {
            var userOwnsTodoCommand = new UserOwnsTodoCommand { TodoId = todoId, UserId = HttpContext.GetUserId() };

            var userOnwsTodoResult = await _mediator.Send(userOwnsTodoCommand);
            return userOnwsTodoResult;
        }
    }
}
