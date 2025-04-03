namespace MemoryLeak.Services.Cache;

public class GoodCacheManager(IMemoryCache cache, ILogger<GoodCacheManager> logger)
{
    /// <summary>
    /// Good implementation
    /// </summary>
    public async Task<T?> GetOrCreate<T>(string cacheKey, T value)
    {
        logger.LogInformation($"Attempting to get or create cache entry with key: {cacheKey}");
        
        //Artificial delay to simulate async I/O-bound operation
        await Task.Delay(1000);
        
        if (cache.TryGetValue(cacheKey, out T? cachedEntry))
        {
            logger.LogInformation($"Cache hit for key: {cacheKey}");
            return cachedEntry;
        }
        
        logger.LogWarning($"Cache miss for key: {cacheKey}. Adding to cache.");

        // We're setting some parameters! Expiration, extend lifespan, size of cache
        cache.Set(cacheKey, cachedEntry, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
            SlidingExpiration = TimeSpan.FromMinutes(5),
            Size = 1
        });
        
        logger.LogInformation($"Added new value to cache with key: {cacheKey}");

        return cachedEntry;
    }
    
    public void ClearCache(string key)
    {
        cache.Remove(key);
    }
}