namespace SuperSimpleQueue
{
    public interface IQueueManager
    {
        public Task CreateQueueAsync(string queueName);

        public Task DeleteQueueAsync(string queueName);

        public Task<bool> CheckIfQueueExistAsync(string queueName);

        public IQueueSender GetSender(string queueName);

        public IQueueClient GetClient(string queueName);

        public IQueueListener GetListener(string queueName);
    }
}
