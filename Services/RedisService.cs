using StackExchange.Redis;

namespace Websocket_UI.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        private readonly IDatabase _database;

        public RedisService(ConfigurationOptions connectionString)
        {
            try
            {
                _redisConnection = ConnectionMultiplexer.Connect(connectionString);
                _database = _redisConnection.GetDatabase();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public void Set(string key, string value)
        {
            _database.StringSet(key, value);
        }

        public string Get(string key)
        {
            return _database.StringGet(key);
        }

        public void Dispose()
        {
            _redisConnection?.Dispose();
        }
        public void Delete(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
