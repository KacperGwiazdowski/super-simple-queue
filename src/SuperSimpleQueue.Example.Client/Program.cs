// See https://aka.ms/new-console-template for more information
using SuperSimpleQueue.Embedded;

var queueName = "queue";

var queueManager = new LocalQueueManager();

if (!await queueManager.CheckIfQueueExistAsync(queueName))
{
    await queueManager.CreateQueueAsync(queueName);
}

var listener = queueManager.GetListener(queueName);

var client = queueManager.GetClient(queueName);

Console.WriteLine("Listener started.");

listener.MessagesReadyForProcessingAsync += async () =>
{
    Console.WriteLine("New message triggered!");

    var message = await client.GetNextMessageAsync();

    await client.CompleteMessageAsync(message!.MessageId);
};

listener.StartListening();

await Task.Delay(-1);