using LiteDB;
using SuperSimpleQueue.Core.Configuration;

namespace SuperSimpleQueue.Core.Utils
{
    public class ConnectionStringBuilder
    {
        public static ConnectionString GetLiteDbConnectionString(ConnectionStringConfiguration configuration)
        {
            return new ConnectionString
            {
                Filename = configuration.FileName,
                Connection = configuration.ConnectionType,
            };
        }
    }
}
