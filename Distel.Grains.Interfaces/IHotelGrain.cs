using Distel.Grains.Interfaces.Models;
using Orleans;

namespace Distel.Grains.Interfaces;

public interface IHotelGrain : IGrainWithStringKey
{
    Task<string> WelcomeGreetingAsync(string guestName);
    Task<string> GetKey();
    Task<decimal> ComputeDue(string guestName);
    Task OnBoardFromOtherHotel(IHotelGrain fromHotel, string guestName);
    Task<string> CheckInGuest(UserCheckIn userCheckIn);
    Task AssociatePartner(Partner partner);
}