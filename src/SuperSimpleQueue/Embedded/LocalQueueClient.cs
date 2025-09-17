using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Embedded
{
    public class LocalQueueClient(string queueName, IMessageService messageService) : IQueueClient
    {
        public string QueueName => queueName;

        public async Task<bool> CompleteMessageAsync(Guid messageId)
        {
            return await Task.Run(() => messageService.CompleteMessage(QueueName, messageId));
        }

        public async Task<int> CompleteMessageBatchAsync(IEnumerable<Guid> messageIds)
        {
            return await Task.Run(() => messageService.CompleteMessageBatch(QueueName, messageIds));
        }

        public async Task<MessageModel?> GetNextMessageAsync()
        {
            return await Task.Run(() => messageService.GetNextMessage(queueName));
        }

        public async Task<IEnumerable<MessageModel>> GetNextMessageBatchAsync(int batchSize = 10)
        {
            return await Task.Run(() => messageService.GetNextMessageBatch(queueName, batchSize));
        }
    }
}
