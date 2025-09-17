using SuperSimpleQueue.Core.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace SuperSimpleQueue.Remote
{
    internal class RemoteQueueSender(string queueName, HttpClient httpClient) : IQueueSender
    {
        private readonly HttpClient _httpClient = httpClient;

        public string QueueName => queueName;

        public async Task<Guid> SendMessageAsync(AddMessageModel message)
        {
            using var content = JsonContent.Create(message);

            var result = await _httpClient.PostAsync($"/queues/{queueName}/messages", content);

            if (!result.IsSuccessStatusCode) 
            {
                throw new ApplicationException($"Failed to send message to {queueName} queue with: {result.ReasonPhrase}");
            }

            var response = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Guid>(response);
        }

        public async Task<IEnumerable<Guid>> SendMessageBatchAsync(IEnumerable<AddMessageModel> messages)
        {
            using var content = JsonContent.Create(messages);

            var result = await _httpClient.PostAsync($"/queues/{queueName}/messages/batch", content);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Failed to send messages to {queueName} queue with: {result.ReasonPhrase}");
            }

            var response = await result.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Guid>>(response);
        }
    }
}
