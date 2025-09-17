using SuperSimpleQueue.Core.Services;

namespace SuperSimpleQueue.Embedded
{
    public class LocalQueueManager(IQueuesService queuesService, IMessageService messageService) : IQueueManager
    {
        private readonly IQueuesService _queuesService = queuesService;
        private readonly IMessageService _messageService = messageService;

        public async Task<bool> CheckIfQueueExistAsync(string queueName)
        {
            return await Task.Run<bool>(() => _queuesService.CheckIfQueueExist(queueName));
        }

        public async Task CreateQueueAsync(string queueName)
        {
            await Task.Run(() => _queuesService.AddQueue(queueName));
        }

        public async Task DeleteQueueAsync(string queueName)
        {
            await Task.Run(() => _queuesService.RemoveQueue(queueName));
        }

        public IQueueClient GetClient(string queueName)
        {
            return new LocalQueueClient(queueName, _messageService);
        }

        public IQueueSender GetSender(string queueName)
        {

            return new LocalQueueSender(queueName, _messageService);
        }
    }
}
