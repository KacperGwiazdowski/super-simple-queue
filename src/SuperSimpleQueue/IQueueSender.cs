using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue
{
    public interface IQueueSender
    {
        public string QueueName { get; } 

        public Task<Guid> SendMessageAsync(AddMessageModel message);

        public Task<IEnumerable<Guid>> SendMessageBatchAsync(IEnumerable<AddMessageModel> messages);

    }
}