namespace MemoryLeak.Services;

public class HotelService(GoodCacheManager goodCacheManager, BadCacheManager badCacheManager) : IHotelService
{
    private const string HotelCacheKey = "Hotel";
    
    public async Task<Hotel?> GetOrCreateHotelAsyncBad(Guid hotelId, Hotel value) =>
        await badCacheManager.GetOrCreate($"{HotelCacheKey}_{hotelId}", value);
    
    public async Task<Hotel?> GetOrCreateHotelAsyncGood(Guid hotelId, Hotel value) =>
        await goodCacheManager.GetOrCreate($"{HotelCacheKey}_{hotelId}", value);
}