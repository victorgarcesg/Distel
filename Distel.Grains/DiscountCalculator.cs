using Distel.Grains.Interfaces;
using Orleans.Concurrency;

namespace Distel.Grains;

[StatelessWorker]
public class DiscountCalculator : Grain, IDiscountCalculator
{
    public Task<decimal> ComputeDiscount(decimal price)
    {
        var discount = price switch
        {
            > 100 => price * .1M,
            > 50 => price * .05M,
            _ => 0
        };
        return Task.FromResult(discount);
    }
}
