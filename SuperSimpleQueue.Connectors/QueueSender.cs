using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Connectors
{
    public class QueueSender(string queueName, IMessageService messageService) : IQueueSender
    {
        public string QueueName => queueName;

        public Guid SendMessage(AddMessageModel message)
        {
            return messageService.AddMessage(QueueName, message);
        }

        public IEnumerable<Guid> SendMessageBatch(IEnumerable<AddMessageModel> messages)
        {
            return messageService.AddMessageBatch(QueueName, messages);
        }
    }
}
