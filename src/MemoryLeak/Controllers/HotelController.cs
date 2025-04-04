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
    [HttpGet("good")]
    public async Task<IResult> GetOrCreateGood()
    {
        //Random Id (but normally it should be in the request)
        var hotelId = Guid.NewGuid();
        var hotel = await hotelService.GetOrCreateHotelAsyncGood(hotelId, new Hotel { Id = hotelId, Name = "Hotel A" });

        return Results.Ok(hotel);
    }
    
    /// <summary>
    /// Scenario 1 with Bad Caching
    /// </summary>
    [HttpGet("bad")]
    public async Task<IResult> GetOrCreateBad()
    {
        //Random Id (but normally it should be in the request)
        var hotelId = Guid.NewGuid();
        
        //creating object with 10MB payload
        var hotel = await hotelService.GetOrCreateHotelAsyncBad(hotelId, new Hotel { Id = hotelId, Name = "Hotel A", Payload = new byte[10 * 1024 * 1024] });

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