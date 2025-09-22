namespace SuperSimpleQueue.Remote
{
    internal class RemoteQueueManager(HttpClient httpClient) : IQueueManager
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<bool> CheckIfQueueExistAsync(string queueName)
        {
            var result = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, $"/queues/{queueName}"));

            return result.IsSuccessStatusCode;
        }

        public async Task CreateQueueAsync(string queueName)
        {
            var result = await _httpClient.PostAsync($"/queues/{queueName}", null);
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to create {queueName} queue with: {result.ReasonPhrase}");
            }
        }

        public async Task DeleteQueueAsync(string queueName)
        {
            var result = await _httpClient.DeleteAsync($"/queues/{queueName}");
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to remove {queueName} queue with: {result.ReasonPhrase}");
            }
        }

        public IQueueClient GetClient(string queueName)
        {
            return new RemoteQueueClient(queueName, _httpClient);
        }

        public IQueueListener GetListener(string queueName)
        {
            throw new NotImplementedException();
        }

        public IQueueSender GetSender(string queueName)
        {
            return new RemoteQueueSender(queueName, _httpClient);
        }
    }
}
