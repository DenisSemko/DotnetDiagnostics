namespace MemoryLeak.Controllers;

/// <summary>
/// Controller for Hotel operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HotelController(IHotelService hotelService)
{
    /// <summary>
    /// Scenario 1 with Good Caching
    /// </summary>
    [HttpGet("good/{hotelId}")]
    public async Task<IResult> GetOrCreateGood(Guid hotelId)
    {
        var hotel = await hotelService.GetOrCreateHotelAsyncGood(hotelId, new Hotel { Id = hotelId, Name = "Hotel A" });

        return Results.Ok(hotel);
    }
    
    /// <summary>
    /// Scenario 1 with Bad Caching
    /// </summary>
    [HttpGet("bad/{hotelId}")]
    public async Task<IResult> GetOrCreateBad(Guid hotelId)
    {
        var hotel = await hotelService.GetOrCreateHotelAsyncBad(hotelId, new Hotel { Id = hotelId, Name = "Hotel A" });

        return Results.Ok(hotel);
    }
    
    // /// <summary>
    // /// Scenario 2 with Background Job Queue
    // /// </summary>
    // [HttpPost]
    // public IResult CreateHotel([FromBody] BookingRequest bookingRequest)
    // {
    //     _processor.Enqueue(bookingRequest);
    //     return Results.Accepted();
    // }
}