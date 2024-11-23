using Cms.Api.Cache.Abstract;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Cms.Api.Cache.Concrate
{
    public class CacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IDatabase _cache;

        public CacheService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _cache = redisConnection.GetDatabase();
        }

        public async Task<TEntity> ConfigureSetGetAsync<TEntity>(string key, Func<Task<TEntity>> func, TimeSpan? offset = null) where TEntity : class
        {
            var jsonData = await _cache.StringGetAsync(key);

            if (jsonData.HasValue)
                return JsonConvert.DeserializeObject<TEntity>(jsonData);

            var data = await func.Invoke().ConfigureAwait(false);

            if (data != null)
            {
                offset ??= TimeSpan.FromMinutes(15);

                jsonData = JsonConvert.SerializeObject(data);

                await _cache.StringSetAsync(key, jsonData, offset).ConfigureAwait(false);
            }

            return data;
        }

        public async Task<IEnumerable<TEntity>> ConfigureSetGetAsync<TEntity>(string key, Func<Task<IEnumerable<TEntity>>> func, TimeSpan? offset = null) where TEntity : class
        {
            var jsonData = await _cache.StringGetAsync(key);

            if (jsonData.HasValue)
                return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(jsonData);

            var data = await func.Invoke().ConfigureAwait(false);

            if (data != null)
            {
                offset ??= TimeSpan.FromMinutes(15);

                jsonData = JsonConvert.SerializeObject(data);

                await _cache.StringSetAsync(key, jsonData, offset).ConfigureAwait(false);
            }

            return data;
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key).ConfigureAwait(false);
        }

        public async Task ClearAll()
        {
            var redisEndpoints = _redisConnection.GetEndPoints(true);

            foreach (var redisEndpoint in redisEndpoints)
            {
                var redisServer = _redisConnection.GetServer(redisEndpoint);

                await redisServer.FlushAllDatabasesAsync().ConfigureAwait(false);
            }
        }

        public static string GetCacheKey(HttpContext httpContext, object? reqModel = null)
        {
            var seperator = '-';

            var cache = httpContext.Request.Path;

            if (reqModel != null)
                foreach (var property in reqModel.GetType().GetProperties())
                {
                    cache += $"{seperator}{property.GetValue(reqModel)}";
                }

            return cache;
        }
    }
}
