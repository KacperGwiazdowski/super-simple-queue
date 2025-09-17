namespace SuperSimpleQueue.Connectors
{
    public interface IQueueManager
    {
        public void CreateQueue(string queueName);

        public void DeleteQueue(string queueName);

        public bool CheckIfQueueExist(string queueName);

        public IQueueSender GetSender(string queueName);

        public IQueueClient GetClient(string queueName);
    }
}
