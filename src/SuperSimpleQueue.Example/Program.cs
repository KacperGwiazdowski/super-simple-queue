// See https://aka.ms/new-console-template for more information
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Embedded;

var queueName = "queue";

var queueManager = new LocalQueueManager();

if (!await queueManager.CheckIfQueueExistAsync(queueName))
{
    await queueManager.CreateQueueAsync(queueName);
}

var sender = queueManager.GetSender(queueName);

await sender.SendMessageAsync(new AddMessageModel($"Test message body {DateTime.Now}"));