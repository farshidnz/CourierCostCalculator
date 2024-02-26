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
        var weightLimit = GetWeightLimitForParcelSize(parcel);
        var cost = GetBaseCostForParcelSize(parcel);

        if (parcel.Weight > weightLimit)
        {
            var extraWeight = parcel.Weight - weightLimit;
            var extraWeightCharge = parcel.Weight >= ParcelSize.Heavy.WeightLimit() ? 1 : 2;
            return cost + extraWeight * extraWeightCharge;
        }

        return cost;
    }

    private static double GetBaseCostForParcelSize(Parcel parcel)
    {
        if (parcel.Weight >= ParcelSize.Heavy.WeightLimit())
            return ParcelSize.Heavy.Cost();
        
        var parcelDimension = parcel.Dimension;
        if (parcelDimension < ParcelSize.Small.DimensionLimit())
            return ParcelSize.Small.Cost();
        if (parcelDimension < ParcelSize.Medium.DimensionLimit())
            return ParcelSize.Medium.Cost();
        if (parcelDimension < ParcelSize.Large.DimensionLimit())
            return ParcelSize.Large.Cost();
        return ParcelSize.ExtraLarge.Cost();
    }

    private static double GetWeightLimitForParcelSize(Parcel parcel)
    {
        if (parcel.Weight >= ParcelSize.Heavy.WeightLimit())
            return ParcelSize.Heavy.WeightLimit();
        
        var parcelDimension = parcel.Dimension;
        if (parcelDimension < ParcelSize.Small.DimensionLimit())
            return ParcelSize.Small.WeightLimit();
        else if (parcelDimension < ParcelSize.Medium.DimensionLimit())
            return ParcelSize.Medium.WeightLimit();
        else if (parcelDimension < ParcelSize.Large.DimensionLimit())
            return ParcelSize.Large.WeightLimit();
        else
            return ParcelSize.ExtraLarge.WeightLimit();
    }
}