// See https://aka.ms/new-console-template for more information
using LiteDB;
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;
using SuperSimpleQueue.Core.Utils;
using SuperSimpleQueue.Embedded;

var queueName = "queue";

var db = new LiteDatabase(ConnectionStringBuilder.GetLiteDbConnectionString(new SuperSimpleQueue.Core.Configuration.ConnectionStringConfiguration()));

var queueManager = new LocalQueueManager(new QueueService(db), new MessageService(db));

if (!await queueManager.CheckIfQueueExistAsync(queueName))
{
    await queueManager.CreateQueueAsync(queueName);
}

var sender = queueManager.GetSender(queueName);

await sender.SendMessageAsync(new AddMessageModel($"test message body {DateTime.Now}"));

var client = queueManager.GetClient(queueName);

var message = await client.GetNextMessageAsync();

await client.CompleteMessageAsync(message!.MessageId);