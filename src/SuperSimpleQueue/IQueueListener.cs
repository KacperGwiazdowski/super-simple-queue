namespace SuperSimpleQueue
{
    public interface IQueueListener
    {
        public string QueueName { get; }

        void StartListening();
        void StopListening();

        event Func<Task>? MessagesReadyForProcessingAsync;
    }
}
