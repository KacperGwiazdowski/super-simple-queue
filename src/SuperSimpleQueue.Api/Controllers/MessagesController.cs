using Microsoft.AspNetCore.Mvc;
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Api.Controllers
{
    [ApiController]
    [Route("queues/{queueName}/[controller]")]
    public class MessagesController(IMessageService messageService) : ControllerBase
    {
        private readonly IMessageService _messageService = messageService;

        [HttpPost]
        public ActionResult AddMessage([FromRoute] string queueName, AddMessageModel messageModel)
        {
            var messageId = _messageService.AddMessage(queueName, messageModel);
            return Accepted(messageId);
        }

        [HttpPost("batch")]
        public ActionResult AddMessageBatch([FromRoute] string queueName, IEnumerable<AddMessageModel> messageModels)
        {
            var messageIds = _messageService.AddMessageBatch(queueName, messageModels);
            return Accepted(messageIds);
        }

        [HttpPost("{messageId}/complete")]
        public ActionResult CompleteMessage([FromRoute] string queueName, [FromRoute] Guid messageId)
        {
            _messageService.CompleteMessage(queueName, messageId);
            return Ok();
        }

        [HttpPost("batch/complete")]
        public ActionResult CompleteMessageBatch([FromRoute] string queueName, [FromBody] IEnumerable<Guid> messageIds)
        {
            var count = _messageService.CompleteMessageBatch(queueName, messageIds);
            return Ok(count);
        }

        [HttpGet("next")]
        public ActionResult<MessageModel> GetNextMessage([FromRoute] string queueName)
        {
            var msg = _messageService.GetNextMessage(queueName);

            if (msg == null)
            {
                return NoContent();
            }

            return Ok(msg);
        }

        [HttpGet("batch/next")]
        public ActionResult<IEnumerable<MessageModel>> GetNextMessage([FromRoute] string queueName, [FromQuery] int batchSize = 10)
        {
            var batch = _messageService.GetNextMessageBatch(queueName, batchSize);

            if (batch == null || !batch.Any())
            {
                return NoContent();
            }

            return Ok(batch);
        }

    }
}
