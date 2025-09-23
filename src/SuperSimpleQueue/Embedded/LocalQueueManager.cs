using LiteDB;
using SuperSimpleQueue.Core.Services;
using SuperSimpleQueue.Core.Utils;
using SuperSimpleQueue.Core.Configuration;

namespace SuperSimpleQueue.Embedded
{
    public class LocalQueueManager : IQueueManager
    {
        public LocalQueueManager(ConnectionStringConfiguration? connectionStringConfiguration = null)
        {
            _connectionStringConfiguration = connectionStringConfiguration ?? new ConnectionStringConfiguration();
            var db = new LiteDatabase(ConnectionStringBuilder.GetLiteDbConnectionString(_connectionStringConfiguration));
            Console.WriteLine($"Queue path set to {_connectionStringConfiguration.FileName}");
            _queuesService = new QueueService(db);
            _messageService = new MessageService(db);
        }

        private readonly ConnectionStringConfiguration _connectionStringConfiguration;
        private readonly IQueuesService _queuesService;
        private readonly IMessageService _messageService;

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

        public IQueueListener GetListener(string queueName)
        {
            return new LocalQueueListener(queueName, _connectionStringConfiguration, _messageService);
        }
    }
}
