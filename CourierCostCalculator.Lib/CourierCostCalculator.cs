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
        var weightLimit = GetWeightLimitForParcelSize(parcel.Dimension);
        var cost =  GetBaseCostForParcelSize(parcel.Dimension);
        
        if (parcel.Weight > weightLimit)
        {
            var extraWeight = parcel.Weight - weightLimit;
           return cost + extraWeight * 2;
        }

        return cost;
    }
    
    private static double GetBaseCostForParcelSize(double maxDimension)
    {
        if (maxDimension < ParcelSize.Small.DimensionLimit())
            return ParcelSize.Small.Cost();
        if (maxDimension < ParcelSize.Medium.DimensionLimit())
            return ParcelSize.Medium.Cost();
        if (maxDimension < ParcelSize.Large.DimensionLimit())
            return ParcelSize.Large.Cost();
        return ParcelSize.ExtraLarge.Cost();
    }
    
    private static double GetWeightLimitForParcelSize(double maxDimension)
    {
        if (maxDimension < ParcelSize.Small.DimensionLimit())
            return ParcelSize.Small.WeightLimit();
        else if (maxDimension < ParcelSize.Medium.DimensionLimit())
            return ParcelSize.Medium.WeightLimit();
        else if (maxDimension < ParcelSize.Large.DimensionLimit())
            return ParcelSize.Large.WeightLimit();
        else
            return ParcelSize.ExtraLarge.WeightLimit();
    }
}