using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Core.Services
{
    public interface IMessageService
    {
        public Guid AddMessage(string queue, AddMessageModel message);

        public IEnumerable<Guid> AddMessageBatch(string queue, IEnumerable<AddMessageModel> messages);

        public MessageModel GetMessage(string queue, Guid messageId);

        public MessageModel? GetNextMessage(string queue);

        public IEnumerable<MessageModel> GetNextMessageBatch(string queue, int batchSize = 10);

        public bool CompleteMessage(string queue, Guid messageId);

        public int CompleteMessageBatch(string queue, IEnumerable<Guid> messageIds);

        public bool AreMessagesAvailable(string queue);
    }
}
