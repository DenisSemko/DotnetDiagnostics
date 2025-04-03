namespace MemoryLeak.Services.Cache;

public class BadCacheManager(IMemoryCache cache, ILogger<BadCacheManager> logger)
{
    /// <summary>
    /// Bad implementation
    /// </summary>
    public async Task<T?> GetOrCreate<T>(string cacheKey, T? value)
    {
        logger.LogInformation($"Attempting to get or create cache entry with key: {cacheKey}");
        
        //Artificial delay to simulate async I/O-bound operation
        await Task.Delay(1000);
        
        if (cache.TryGetValue(cacheKey, out T? cachedValue))
        {
            logger.LogInformation($"Cache hit for key: {cacheKey}");
            return cachedValue;
        }

        logger.LogWarning($"Cache miss for key: {cacheKey}. Adding to cache.");
        
        // We don't set any expiration params etc, our object will live in cache forever!
        cache.Set(cacheKey, value);
        
        logger.LogInformation($"Added new value to cache with key: {cacheKey}");
        
        return value;
    }
}