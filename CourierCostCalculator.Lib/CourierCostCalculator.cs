using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Lib;

public static class CourierCostMultipleParcelsCalculator
{    public static double CalculateCost(Parcel parcel)
    {
        var maxDimension = Math.Max(parcel.Length, Math.Max(parcel.Width, parcel.Height));

        return maxDimension switch
        {
            < 10 => 3,
            < 50 => 8,
            < 100 => 15,
            _ => 25
        };
    }
}
