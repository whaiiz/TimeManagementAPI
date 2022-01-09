using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeManagementAPI.Filters;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Controllers
{
    [ApiController]
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    [EnableCors]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TaskController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskModel task)
        {
            await _repository.Create(task);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id) => Ok(await _repository.GetById(id));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskModel task)
        {
            await _repository.Update(task);
            return Ok();
        }

        [HttpPut("UpdateDate")]
        public async Task<IActionResult> UpdateDate(string id, DateTime date)
        {
            await _repository.UpdateDate(id, date);
            return Ok();
        }

        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus(string id, string status)
        {
            await _repository.UpdateStatus(id, status);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
