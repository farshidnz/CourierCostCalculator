namespace CourierCostCalculator.Lib.Models;

public record TotalCost(List<ParcelCalculatedCost> Parcels)
{
    public bool SpeedyShipping { get; set; } = false;

    public double FinalPrice
    {
        get
        {
            var sum = Parcels.Sum(p => p.Cost);
            var priceAfterDiscount = sum - TotalDiscount;
            return SpeedyShipping ? priceAfterDiscount * 2 : priceAfterDiscount;
        }
    }

    public double TotalDiscount { get; set; }
};