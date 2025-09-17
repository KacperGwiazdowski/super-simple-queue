using Microsoft.AspNetCore.Mvc;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueuesController(IQueuesService queuesService) : ControllerBase
    {
        private readonly IQueuesService _queuesService = queuesService;
        [HttpPost("{queueName}")]
        public IActionResult AddQueue(string queueName)
        {
            _queuesService.AddQueue(queueName);

            return Ok();
        }

        [HttpHead("{queueName}")]
        public IActionResult CheckQueue(string queueName)
        {
            if(_queuesService.CheckIfQueueExist(queueName))
            {
                return Ok();
            }
            return BadRequest();

        }

        [HttpDelete("{queueName}")]
        public IActionResult DeleteQueue(string queueName)
        {
            _queuesService.RemoveQueue(queueName);

            return Ok();
        }

        [HttpDelete("{queueName}/schema")]
        public IActionResult AddQueueSchema(string queueName, string schema)
        {
            throw new NotImplementedException();
        }

    }
}
