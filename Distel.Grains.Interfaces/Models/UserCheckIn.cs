namespace Distel.Grains.Interfaces.Models;

[GenerateSerializer]
public record UserCheckIn(string UserId, DateTime CheckInDate);
