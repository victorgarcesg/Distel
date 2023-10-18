using Distel.Grains.Interfaces;
using Distel.Grains.Interfaces.Models;
using Microsoft.Extensions.Logging;
using Orleans.Concurrency;
using Orleans.Runtime;

namespace Distel.Grains;

[Reentrant]
public class HotelGrain : Grain, IHotelGrain
{
    private readonly ILogger _logger;
    private readonly IPersistentState<List<UserCheckIn>> _checkedInGuests;
    private readonly IPersistentState<List<Partner>> _partners;

    public HotelGrain(ILogger<HotelGrain> logger,
        [PersistentState("checkedInGuests")] IPersistentState<List<UserCheckIn>> checkedInGuests,
        [PersistentState("partners")] IPersistentState<List<Partner>> partners)
    {
        _logger = logger;
        _checkedInGuests = checkedInGuests;
        _partners = partners;
    }

    public Task<decimal> ComputeDue(string guestName) => Task.FromResult(100.00M);

    public Task<string> GetKey() => Task.FromResult(this.GetPrimaryKeyString());

    public async Task OnBoardFromOtherHotel(IHotelGrain fromHotel, string guestName)
    {
        _logger.LogInformation($"Onboarding the guest from other hotel {guestName}");
        await fromHotel.ComputeDue(guestName);
        _logger.LogInformation("2");
    }

    public Task<string> WelcomeGreetingAsync(string guestName)
    {
        _logger.LogInformation($"\n WelcomeGreetingAsync message received: greeting = '{guestName}'");
        return Task.FromResult($"Dear {guestName}, We welcome you to Distel and hope you enjoy a comfortable stay at our hotel.");
    }

    public async Task<string> CheckInGuest(UserCheckIn userCheckIn)
    {
        _checkedInGuests.State.Add(userCheckIn);
        await _checkedInGuests.WriteStateAsync();
        return string.Empty;
    }

    public async Task AssociatePartner(Partner partner)
    {
        if (!_partners.State.Any(e => e.Id == partner.Id))
        {
            _partners.State.Add(partner);
            await _partners.WriteStateAsync();
        }
    }
}
