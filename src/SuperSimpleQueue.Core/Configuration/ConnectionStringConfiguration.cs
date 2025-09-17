using LiteDB;

namespace SuperSimpleQueue.Core.Configuration
{
    public class ConnectionStringConfiguration()
    {
        public string FileName { get; set; } = "queues.db";

        public ConnectionType ConnectionType { get; set; } = ConnectionType.Shared;
    }
}
