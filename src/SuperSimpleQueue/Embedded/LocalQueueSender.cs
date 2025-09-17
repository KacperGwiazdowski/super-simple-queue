using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Embedded
{
    public class LocalQueueSender(string queueName, IMessageService messageService) : IQueueSender
    {
        public string QueueName => queueName;

        public async Task<Guid> SendMessageAsync(AddMessageModel message)
        {
            return await Task.Run(() => messageService.AddMessage(QueueName, message));
        }

        public async Task<IEnumerable<Guid>> SendMessageBatchAsync(IEnumerable<AddMessageModel> messages)
        {
            return await Task.Run(() => messageService.AddMessageBatch(QueueName, messages));
        }
    }
}
