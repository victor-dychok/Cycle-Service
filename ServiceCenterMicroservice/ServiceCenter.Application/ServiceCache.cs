using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace ServiceCenter.Application
{
    public class ServiceCache<TItem> : IServiceCache<TItem> where TItem : class
    {
        private readonly IDistributedCache _cache;
        private readonly RedisService _redisServer;
        private string itemName => typeof(TItem).Name;
        public ServiceCache(IDistributedCache cache, RedisService redis)
        {
            _cache = cache;
            _redisServer = redis;
        }

        private int AbsoluteExpiration = 10;
        private int SlidingExpiration = 5;
        private string CreateCacheKey<TRequest>(TRequest request)
        {
            return $"{itemName}_{JsonSerializer.Serialize(request)}";
        }

        private string CreateCacheKey<TRequest>(TRequest request, string secondKey)
        {
            return $"{itemName}_{JsonSerializer.Serialize(request)}_{secondKey}";
        }

        public void Set<TRequest>(TRequest request, string secondKey, TItem item)
        {
            var jsonItem = JsonSerializer.Serialize(item);

            _cache.SetString(CreateCacheKey(request, secondKey), jsonItem, new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = new DateTimeOffset(DateTime.Now, new TimeSpan(0, 0, AbsoluteExpiration, 0)),
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
            });
        }

        public void Set<TRequest>(TRequest request, TItem item)
        {
            var jsonItem = JsonSerializer.Serialize(item);

            _cache.SetString(CreateCacheKey(request), jsonItem, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AbsoluteExpiration),
                SlidingExpiration = TimeSpan.FromMinutes(SlidingExpiration)
            });
        }

        public bool TryGetValue<TRequest>(TRequest request, out TItem? item)
        {
            var itemString = _cache.GetString(CreateCacheKey(request));
            if (string.IsNullOrWhiteSpace(itemString))
            {
                item = null;
                return false;
            }
            item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
            });
            return true;
        }

        public bool TryGetValue<TRequest>(TRequest request, string secondKey, out TItem? item)
        {
            var itemString = _cache.GetString(CreateCacheKey(request, secondKey));
            if (string.IsNullOrWhiteSpace(itemString))
            {
                item = null;
                return false;
            }
            item = JsonSerializer.Deserialize<TItem>(itemString, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false,
            });
            return true;
        }

        public void DeleteItem<TRequest>(TRequest request)
        {
            _cache.Remove(CreateCacheKey(request));
        }

        public void DeleteItem<TRequest>(TRequest request, string secondKey)
        {
            _cache.Remove(CreateCacheKey(request, secondKey));
        }

        public void Clear()
        {
            var keys = _redisServer.GetAllKeys(itemName).ToArray();

            foreach (var redisKey in keys)
            {
                _cache.Remove(redisKey);
            }
        }
    }
}
