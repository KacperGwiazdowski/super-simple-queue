using SuperSimpleQueue.Core.Configuration;
using SuperSimpleQueue.Core.Services;
using System.Threading;

namespace SuperSimpleQueue.Embedded
{
    public class LocalQueueListener(string queueName, ConnectionStringConfiguration connectionStringConfiguration, IMessageService messageService) : IQueueListener
    {
        private readonly ConnectionStringConfiguration _connectionStringConfiguration = connectionStringConfiguration;
        private FileSystemWatcher? _watcher;
        private readonly IMessageService _messageService = messageService;
        private DateTime _lastTriggerTime = DateTime.MinValue;
        private CancellationTokenSource? _cts;
        private Task? _timerTask;

        public string QueueName { get; } = queueName;

        public event Func<Task>? MessagesReadyForProcessingAsync;

        public void StartListening()
        {
            var directory = Path.GetDirectoryName(_connectionStringConfiguration.FileName)!;
            var fileName = Path.GetFileName(_connectionStringConfiguration.FileName)!;

            _watcher = new FileSystemWatcher(directory, fileName)
            {
                NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite,
                EnableRaisingEvents = true
            };

            _watcher.Changed += OnDbFileSizeChanged;

            _cts = new CancellationTokenSource();
            _timerTask = RunPeriodicCheck(_cts.Token);
        }

        private async Task RunPeriodicCheck(CancellationToken token)
        {
            var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            try
            {
                while (await timer.WaitForNextTickAsync(token))
                {
                    await CheckQueueMessages();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }

        private async void OnDbFileSizeChanged(object sender, FileSystemEventArgs e)
        {
            var now = DateTime.UtcNow;
            if (now - _lastTriggerTime < TimeSpan.FromSeconds(1))
                return;
            _lastTriggerTime = now;

            await Task.Delay(50);

            await CheckQueueMessages();
        }

        private async Task CheckQueueMessages()
        {
            if (!_messageService.AreMessagesAvailable(QueueName))
                return;

            if (MessagesReadyForProcessingAsync != null)
            {
                await Task.WhenAll(
                    MessagesReadyForProcessingAsync.GetInvocationList()
                        .Cast<Func<Task>>()
                        .Select(handler => handler())
                );
            }
        }

        public void StopListening()
        {
            _watcher?.Dispose();
            _cts?.Cancel();
            try { _timerTask?.Wait(); } catch { }
            _cts?.Dispose();
        }
    }
}
