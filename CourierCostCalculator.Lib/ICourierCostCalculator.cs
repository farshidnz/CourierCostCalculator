using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Lib;

public interface ICourierCostCalculator
{
    public TotalCost CalculateCost(IEnumerable<Parcel> parcels, bool speedyShipping = false);
    public double CalculateCost(Parcel parcel);
}