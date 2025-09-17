using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Connectors
{
    public class QueueClient(string queueName, IMessageService messageService) : IQueueClient
    {
        public string QueueName => queueName;

        public bool CompleteMessage(Guid messageId)
        {
            return messageService.CompleteMessage(QueueName, messageId);
        }

        public int CompleteMessageBatch(IEnumerable<Guid> messageIds)
        {
            return messageService.CompleteMessageBatch(QueueName, messageIds);
        }

        public MessageModel? GetNextMessage()
        {
            return messageService.GetNextMessage(queueName);
        }

        public IEnumerable<MessageModel> GetNextMessageBatch(int batchSize = 10)
        {
            return messageService.GetNextMessageBatch(queueName, batchSize);
        }
    }
}
