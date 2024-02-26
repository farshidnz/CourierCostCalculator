using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Lib;

public static class CourierCostMultipleParcelsCalculator
{
    public static TotalCost CalculateCost(IEnumerable<Parcel> parcels, bool speedyShipping = false)
    {
        var parcelCosts = new List<ParcelCalculatedCost>();
        foreach (var parcel in parcels)
        {
            var cost = CalculateCost(parcel);
            parcelCosts.Add(new ParcelCalculatedCost(parcel.Name ?? string.Empty, cost));
        }

        return new TotalCost(parcelCosts)
        {
            SpeedyShipping = speedyShipping
        };
    }
    public static double CalculateCost(Parcel parcel)
    {
        var maxDimension = Math.Max(parcel.Length, Math.Max(parcel.Width, parcel.Height));

        return maxDimension switch
        {
            < ParcelSize.Small => 3,
            < ParcelSize.Medium => 8,
            < ParcelSize.Large => 15,
            _ => 25
        };
    }
}