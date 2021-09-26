using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemRepository _repository;

        public WorkItemController(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Create([FromBody] WorkItem workItem)
        {
            await _repository.Create(workItem);
            return Ok();
        }

        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repository.GetAll());
        }

        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _repository.GetById(id));
        }

        public async Task<IActionResult> Update([FromBody] WorkItem workItem)
        {
            await _repository.Update(workItem);
            return Ok();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
