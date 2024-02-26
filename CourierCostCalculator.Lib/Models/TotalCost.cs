namespace CourierCostCalculator.Lib.Models;

public record TotalCost(List<ParcelCalculatedCost> Parcels)
{
    public bool SpeedyShipping { get; set; } = false;

    public double FinalPrice
    {
        get
        {
            var sum = Parcels.Sum(p => p.Cost);
            return SpeedyShipping ? sum * 2 : sum;
        }
    }
};