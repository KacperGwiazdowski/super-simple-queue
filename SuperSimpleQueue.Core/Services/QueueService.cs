using LiteDB;
using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Core.Services
{
    public class QueueService(ILiteDatabase db) : IQueuesService
    {
        private readonly ILiteDatabase _db = db;

        public void AddQueue(string queueName)
        {
            if(_db.CollectionExists(queueName))
            {
                throw new ApplicationException($"Collection {queueName} already exist.");
            }
            
            var collection = _db.GetCollection<MessageModel>(queueName);

            collection.EnsureIndex(nameof(MessageModel.MessageId), true);
            collection.EnsureIndex(nameof(MessageModel.Status));
            collection.EnsureIndex(nameof(MessageModel.CreatedAt));

        }

        public Task AddQueueSchemaAsync(string queueName, QueueSchema queueSchema)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfQueueExist(string queueName)
        {
            return _db.CollectionExists(queueName);
        }

        public void RemoveQueue(string queueName)
        {
            if (!_db.CollectionExists(queueName))
            {
                throw new ApplicationException($"Collection {queueName} does not exist.");
            }

            _db.DropCollection(queueName);
        }
    }
}
