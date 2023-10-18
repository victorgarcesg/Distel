using Distel.Grains.Interfaces;
using Distel.Grains.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace Distel.WebHost;

[Route("api/[controller]")]
[ApiController]
public class DistelController : ControllerBase
{
    private readonly ILogger<DistelController> _logger;
    private readonly IClusterClient _clusterClient;

    public DistelController(ILogger<DistelController> logger, IClusterClient clusterClient)
    {
        _logger = logger;
        _clusterClient = clusterClient;
    }

    [HttpGet("welcome/{hotel}/{guestName}")]
    public async Task<IActionResult> WelcomeGuest([FromRoute] string hotel, [FromRoute] string guestName)
    {
        var hotelGrain = _clusterClient.GetGrain<IHotelGrain>(hotel);
        var greeting = await hotelGrain.WelcomeGreetingAsync(guestName);
        return Ok(greeting);
    }

    [HttpPost("checkin/{hotel}")]
    public async Task<IActionResult> CheckIn([FromRoute] string hotel, [FromBody] UserCheckIn userCheckIn)
    {
        var hotelGrain = _clusterClient.GetGrain<IHotelGrain>(hotel);
        var alottedRoom = await hotelGrain.CheckInGuest(userCheckIn);
        return Ok(alottedRoom);
    }

    [HttpPost("partner/{hotel}/onboard")]
    public async Task<IActionResult> OnboardPartner([FromRoute] string hotel, [FromBody] Partner partner)
    {
        var hotelGrain = _clusterClient.GetGrain<IHotelGrain>(hotel);
        await hotelGrain.AssociatePartner(partner);
        return Ok();
    }
}
