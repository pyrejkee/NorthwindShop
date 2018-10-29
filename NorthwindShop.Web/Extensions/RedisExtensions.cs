using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace NorthwindShop.Web.Extensions
{
    public static class RedisExtensions
    {
        public static List<RedisKey> GetRedisKeys(this IDistributedCache cache)
        {
            var connectionString = "localhost";
            var options = ConfigurationOptions.Parse(connectionString);
            var connection = ConnectionMultiplexer.Connect(options);
            //var db = connection.GetDatabase();
            var endPoint = connection.GetEndPoints().First();
            var keys = connection.GetServer(endPoint).Keys(pattern: "*").ToList();

            return keys;
        }

        public static void CleanRedisStorage(this IDistributedCache cache, List<RedisKey> redisKeys)
        {
            foreach (var redisKey in redisKeys)
            {
                cache.Remove(redisKey);
            }
        }
    }
}
