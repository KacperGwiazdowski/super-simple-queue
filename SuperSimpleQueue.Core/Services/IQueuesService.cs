using SuperSimpleQueue.Core.Models;

namespace SuperSimpleQueue.Core.Services
{
    public interface IQueuesService
    {
        public void AddQueue(string queueName);

        public Task AddQueueSchemaAsync(string queueName, QueueSchema queueSchema);

        public bool CheckIfQueueExist(string queueName);

        public void RemoveQueue(string queueName);
    }
}
