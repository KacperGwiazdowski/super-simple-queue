using LiteDB;
using SuperSimpleQueue.Core.Enumerations;
using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Core.Services
{
    public class MessageService(ILiteDatabase liteDatabase) : IMessageService
    {
        private readonly ILiteDatabase _liteDatabase = liteDatabase;

        public Guid AddMessage(string queue, AddMessageModel message)
        {
           var collection = GetCollection(queue);

            var item = new MessageModel
            {
                MessageId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now,
                Message = message.Message,
                Status = MessageStatus.New,
            };

            collection.Insert(item);

            return item.MessageId;
        }

        public IEnumerable<Guid> AddMessageBatch(string queue, IEnumerable<AddMessageModel> messages)
        {
            var collection = GetCollection(queue);

            var items = messages.Select(x => new MessageModel
            {
                MessageId = Guid.NewGuid(),
                CreatedAt = DateTimeOffset.Now,
                Message = x.Message,
                Status = MessageStatus.New,
            });

            collection.InsertBulk(items);

            return items.Select(x =>  x.MessageId);
        }

        public bool AreMessagesAvailable(string queue)
        {
            var collection = GetCollection(queue);

            return collection.Exists(x => x.Status == MessageStatus.New);
        }

        public bool CompleteMessage(string queue, Guid messageId)
        {
            var collection = GetCollection(queue);

            var msg = GetMessage(queue, messageId);

            return collection.Delete(msg.Id);
        }

        public int CompleteMessageBatch(string queue, IEnumerable<Guid> messageIds)
        {
            throw new NotImplementedException();
        }

        public MessageModel GetMessage(string queue, Guid messageId)
        {
            var collection = GetCollection(queue);
            return collection.FindOne(x => x.MessageId == messageId);
        }

        public MessageModel? GetNextMessage(string queue)
        {
            var collection = GetCollection(queue);
            var nextMessage = collection.FindOne(Query.EQ(nameof(MessageModel.Status), MessageStatus.New.ToString()), Query.Ascending);

            if (nextMessage == null)
            {
                return null;
            }

            nextMessage.Status = MessageStatus.InProgress;

            collection.Update(nextMessage);

            return nextMessage;
        }

        public IEnumerable<MessageModel> GetNextMessageBatch(string queue, int batchSize = 10)
        {
            var collection = GetCollection(queue);
            var batch = collection.Find(Query.EQ(nameof(MessageModel.Status), MessageStatus.New.ToString()), Query.Ascending).ToArray();

            foreach (var item in batch)
            {
                item.Status = MessageStatus.InProgress;
                collection.Update(item);
            }

            return batch;
        }

        private ILiteCollection<MessageModel> GetCollection(string queue)
        {
            if (!_liteDatabase.CollectionExists(queue))
            {
                throw new ApplicationException($"Queue {queue} does not exist.");
            }

            return _liteDatabase.GetCollection<MessageModel>(queue);
        }
    }
}
