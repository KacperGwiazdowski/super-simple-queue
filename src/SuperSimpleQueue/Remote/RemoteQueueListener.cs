namespace SuperSimpleQueue.Remote
{
    public class RemoteQueueListener : IQueueListener
    {
        public string QueueName => throw new NotImplementedException();

        public event Func<Task>? MessagesReadyForProcessingAsync;

        public void StartListening()
        {
            throw new NotImplementedException();
        }

        public void StopListening()
        {
            throw new NotImplementedException();
        }
    }
}
