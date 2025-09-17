// See https://aka.ms/new-console-template for more information
using LiteDB;
using SuperSimpleQueue.Connectors;
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;
using SuperSimpleQueue.Core.Utils;

Console.WriteLine("Hello, World!");

var queueName = "testQueue";

var db = new LiteDatabase(ConnectionStringBuilder.GetLiteDbConnectionString(new SuperSimpleQueue.Core.Configuration.ConnectionStringConfiguration()));

var queueManager = new QueueManager(new QueueService(db), new MessageService(db));

if (!queueManager.CheckIfQueueExist(queueName))
{
    queueManager.CreateQueue(queueName);
}

var sender = queueManager.GetSender(queueName);

sender.SendMessage(new AddMessageModel($"test message {DateTime.Now}"));

var client = queueManager.GetClient(queueName);

var message = client.GetNextMessage();

client.CompleteMessage(message.MessageId);