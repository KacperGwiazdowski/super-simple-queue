# SuperSimpleQueue

**SuperSimpleQueue** is a simple queueing system implementation built on top of [LiteDB](https://www.litedb.org/).  
It is designed as a lightweight, easy-to-use and setup solution for message queuing in .NET applications, without the need for external brokers (like RabbitMQ or Kafka).

---

## 🚀 Features

- Manage queues
- Fully backed by a file-based LiteDB database

---

## 📦 Installation

The library is available on NuGet:

```bash
dotnet add package SuperSimpleQueue
```

## 📝 Usage
A quick example for sending and receiving messages:

```C#
using LiteDB;
using SuperSimpleQueue.Connectors;
using SuperSimpleQueue.Core.Models;
using SuperSimpleQueue.Core.Services;
using SuperSimpleQueue.Core.Utils;
using SuperSimpleQueue.Core.Configuration;

var queueName = "queue";

var db = new LiteDatabase(ConnectionStringBuilder.GetLiteDbConnectionString(Configuration.ConnectionStringConfiguration()));

var queueManager = new QueueManager(new QueueService(db), new MessageService(db));

if (!queueManager.CheckIfQueueExist(queueName))
{
    queueManager.CreateQueue(queueName);
}

var sender = queueManager.GetSender(queueName);

sender.SendMessage(new AddMessageModel($"test message body {DateTime.Now}"));

var client = queueManager.GetClient(queueName);

var message = client.GetNextMessage();

client.CompleteMessage(message.MessageId);
```

## License

[MIT](http://opensource.org/licenses/MIT)