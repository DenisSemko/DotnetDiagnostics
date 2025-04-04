using System.Collections.Concurrent;

namespace MemoryLeak.Services.Cache;

public class BadCacheManager(ILogger<BadCacheManager> logger)
{
    private readonly ConcurrentDictionary<string, object> _cache = new();
    
    /// <summary>
    /// Bad implementation
    /// </summary>
    public async Task<T?> GetOrCreate<T>(string cacheKey, T? value)
    {
        logger.LogInformation($"Attempting to get or create cache entry with key: {cacheKey}");
        
        //Artificial delay to simulate async I/O-bound operation
        await Task.Delay(1000);
        
        if (_cache.TryGetValue(cacheKey, out var cachedValue))
        {
            logger.LogInformation($"Cache hit for key: {cacheKey}");
            return (T?)cachedValue;
        }

        logger.LogWarning($"Cache miss for key: {cacheKey}. Adding to cache.");
        
        // Our object will live in cache forever!
        _cache.GetOrAdd(cacheKey, value!);
        
        logger.LogInformation($"Added new value to cache with key: {cacheKey}");
        
        return value;
    }
}