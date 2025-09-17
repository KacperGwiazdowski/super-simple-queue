using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Connectors
{
    public interface IQueueSender
    {
        public string QueueName { get; } 

        public Guid SendMessage(AddMessageModel message);

        public IEnumerable<Guid> SendMessageBatch(IEnumerable<AddMessageModel> messages);

    }
}