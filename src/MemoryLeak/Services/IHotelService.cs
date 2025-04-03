namespace MemoryLeak.Services;

public interface IHotelService
{
    Task<Hotel?> GetOrCreateHotelAsyncBad(Guid hotelId, Hotel value);
    Task<Hotel?> GetOrCreateHotelAsyncGood(Guid hotelId, Hotel value);
}