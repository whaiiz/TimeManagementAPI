using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeManagementAPI.Commands.Task;
using TimeManagementAPI.Filters;
using TimeManagementAPI.Models;
using TimeManagementAPI.Queries.Task;

namespace TimeManagementAPI.Controllers
{
    [ApiController]
    [CustomExceptionFilter]
    [Authorize]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskModel task) =>
            Ok(await _mediator.Send(new CreateTaskCommand(task, User.Identity.Name)));

        [HttpGet]
        public async Task<IActionResult> GetAll() => 
            Ok(await _mediator.Send(new GetTasksByUserQuery(User.Identity.Name)));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => 
            Ok(await _mediator.Send(new GetTaskByIdQuery(id, User.Identity.Name)));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskModel task)
        {
            await _mediator.Send(new UpdateTaskCommand(task, User.Identity.Name));
            return Ok();
        }

        [HttpPut("UpdateDate")]
        public async Task<IActionResult> UpdateDate(string id, DateTime date)
        {
            await _mediator.Send(new UpdateTaskDateCommand(id, date, User.Identity.Name));
            return Ok();
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(string id, string status)
        {
            await _mediator.Send(new UpdateTaskStatusCommand(id, status, User.Identity.Name));
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteTaskCommand(id, User.Identity.Name));
            return Ok();
        }
    }
}