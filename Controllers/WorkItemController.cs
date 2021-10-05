using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TimeManagementAPI.Filters;
using TimeManagementAPI.Models;
using TimeManagementAPI.Repositories.Interfaces;

namespace TimeManagementAPI.Controllers
{
    [ApiController]
    [CustomExceptionFilter]
    [Route("api/[controller]")]
    public class WorkItemController : ControllerBase
    {
        private readonly IWorkItemRepository _repository;

        public WorkItemController(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WorkItem workItem)
        {
            await _repository.Create(workItem);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repository.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _repository.GetById(id));

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkItem workItem)
        {
            await _repository.Update(workItem);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
