using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Connectors
{
    public class QueueManager(IQueuesService queuesService, IMessageService messageService) : IQueueManager
    {
        private readonly IQueuesService _queuesService = queuesService;
        private readonly IMessageService _messageService = messageService;

        public bool CheckIfQueueExist(string queueName)
        {
            return _queuesService.CheckIfQueueExist(queueName);
        }

        public void CreateQueue(string queueName)
        {
            _queuesService.AddQueue(queueName);
        }

        public void DeleteQueue(string queueName)
        {
            _queuesService.RemoveQueue(queueName);
        }

        public IQueueClient GetClient(string queueName)
        {
            return new QueueClient(queueName,  _messageService);
        }

        public IQueueSender GetSender(string queueName)
        {
            
            return new QueueSender(queueName, _messageService);
        }
    }
}
