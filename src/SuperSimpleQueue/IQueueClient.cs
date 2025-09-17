using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Connectors
{
    public interface IQueueClient
    {
        public string QueueName { get; }

        public MessageModel? GetNextMessage();

        public IEnumerable<MessageModel> GetNextMessageBatch(int batchSize = 10);

        public bool CompleteMessage(Guid messageId);

        public int CompleteMessageBatch(IEnumerable<Guid> messageIds);
    }
}
