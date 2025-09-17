using SuperSimpleQueue.Core.Enumerations;

namespace SuperSimpleQueue.Core.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public required Guid MessageId { get; set; }

        public required string Message { get; set; }

        public required DateTimeOffset CreatedAt { get; set; }

        public required MessageStatus Status { get; set; }
    }
}
