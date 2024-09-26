using Microsoft.Extensions.Caching.Memory;

namespace TMDB_Api.Services
{
    public class CachingService
    {
        private readonly IMemoryCache _cache;
        private readonly IConfiguration _configuration;

        public CachingService(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        public async Task<T> GetOrSet<T>(string key, Func<Task<T>> getItemCallback)
        {
            if (!_cache.TryGetValue(key, out T item))
            {
                item = await getItemCallback();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(_configuration.GetValue<int>("CacheLifetimeMinutes")));
                _cache.Set(key, item, cacheEntryOptions);
            }
            return item;
        }

        public void Invalidate(string key)
        {
            _cache.Remove(key);
        }
    }
}
