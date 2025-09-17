# SuperSimpleQueue

**SuperSimpleQueue** is a simple queueing system implementation built on top of [LiteDB](https://www.litedb.org/).  
It is designed as a lightweight, easy-to-use and setup solution for message queuing in .NET applications, without the need for external brokers (like RabbitMQ or Kafka).


## 🚀 Features

- Manage queues
- Fully backed by a file-based LiteDB database


## 📦 Installation

The library is available on NuGet:

```bash
dotnet add package SuperSimpleQueue
```

## ⚙️ Operating Modes

SuperSimpleQueue can work in two modes:

- **Embedded (Local) mode** – both sender and client access the same LiteDB file directly on a single server.
- **Remote (HTTP) mode** – sender and client communicate over HTTP with a server that hosts LiteDB.



## 📝 Usage
A quick example for sending and receiving messages (local mode):

```C#
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
```

## License

[MIT](http://opensource.org/licenses/MIT)