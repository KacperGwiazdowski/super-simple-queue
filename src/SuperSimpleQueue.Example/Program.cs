// See https://aka.ms/new-console-template for more information
using LiteDB;
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Embedded;

var queueName = "queue";

var queueManager = new LocalQueueManager();

if (!await queueManager.CheckIfQueueExistAsync(queueName))
{
    await queueManager.CreateQueueAsync(queueName);
}

var sender = queueManager.GetSender(queueName);

await sender.SendMessageAsync(new AddMessageModel($"test message body {DateTime.Now}"));

var client = queueManager.GetClient(queueName);

var message = await client.GetNextMessageAsync();

await client.CompleteMessageAsync(message!.MessageId);