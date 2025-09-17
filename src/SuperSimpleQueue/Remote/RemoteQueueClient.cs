using SuperSimpleQueue.Core.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SuperSimpleQueue.Remote
{
    public class RemoteQueueClient(string queueName, HttpClient httpClient) : IQueueClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public string QueueName => queueName;

        public async Task<bool> CompleteMessageAsync(Guid messageId)
        {
            var result = await _httpClient.PostAsync($"/queues/{queueName}/messages/{messageId}/complete", null);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to complete message in {queueName} queue with: {result.ReasonPhrase}");
            }

            return true;
        }

        public async Task<int> CompleteMessageBatchAsync(IEnumerable<Guid> messageIds)
        {
            using var content = JsonContent.Create(messageIds);

            var result = await _httpClient.PostAsync($"/queues/{queueName}/messages/batch/complete", content);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to complete messages in {queueName} queue with: {result.ReasonPhrase}");
            }

            var response = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<int>(response);
        }

        public async Task<MessageModel?> GetNextMessageAsync()
        {
            var result = await _httpClient.GetAsync($"/queues/{queueName}/messages/next");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to get message from {queueName} queue with: {result.ReasonPhrase}");
            }

            var response = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<MessageModel?>(response);
        }

        public async Task<IEnumerable<MessageModel>> GetNextMessageBatchAsync(int batchSize = 10)
        {
            var result = await _httpClient.GetAsync($"/queues/{queueName}/messages/next/batch");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to get messages from {queueName} queue with: {result.ReasonPhrase}");
            }

            var response = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<MessageModel?>>(response);
        }
    }
}
