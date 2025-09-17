using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue
{
    public interface IQueueClient
    {
        public string QueueName { get; }

        public Task<MessageModel?> GetNextMessageAsync();

        public Task<IEnumerable<MessageModel>> GetNextMessageBatchAsync(int batchSize = 10);

        public Task<bool> CompleteMessageAsync(Guid messageId);

        public Task<int> CompleteMessageBatchAsync(IEnumerable<Guid> messageIds);
    }
}
