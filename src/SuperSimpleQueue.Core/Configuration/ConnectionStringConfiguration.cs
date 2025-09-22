using LiteDB;

namespace SuperSimpleQueue.Core.Configuration
{
    public class ConnectionStringConfiguration()
    {
        public string FileName { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "queues.db");

        public ConnectionType ConnectionType { get; set; } = ConnectionType.Shared;
    }
}
