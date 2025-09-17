namespace SuperSimpleQueue.Core.Models
{
    public class AddMessageModel(string message)
    {
        public string Message { get; set; } = message;
    }
}
