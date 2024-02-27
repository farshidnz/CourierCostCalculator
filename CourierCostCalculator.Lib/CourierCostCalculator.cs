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

            var parcelSize = GetParcelSize(parcel);
            parcelCosts.Add(new ParcelCalculatedCost(parcel.Name ?? string.Empty, cost, parcelSize));
        }

        var discountSaving = ApplyDiscounts(parcelCosts);

        return new TotalCost(parcelCosts)
        {
            SpeedyShipping = speedyShipping,
            TotalDiscount = discountSaving
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

    private static ParcelSize GetParcelSize(Parcel parcel)
    {
        if (parcel.Weight >= ParcelSize.Heavy.WeightLimit())
            return ParcelSize.Heavy;

        var parcelDimension = parcel.Dimension;
        if (parcelDimension < ParcelSize.Small.DimensionLimit())
            return ParcelSize.Small;
        else if (parcelDimension < ParcelSize.Medium.DimensionLimit())
            return ParcelSize.Medium;
        else if (parcelDimension < ParcelSize.Large.DimensionLimit())
            return ParcelSize.Large;
        else
            return ParcelSize.ExtraLarge;
    }

    private static double ApplyDiscounts(List<ParcelCalculatedCost> parcelCosts)
    {
        var totalDiscount = 0.0;

        var groupedBySize = parcelCosts.GroupBy(p => p.Size);

        var smallParcels = groupedBySize
            .FirstOrDefault(g => g.Key == ParcelSize.Small);

        if (smallParcels != null)
        {
            totalDiscount += ApplyDiscount(smallParcels, 4);
        }

        var mediumParcels = groupedBySize
            .FirstOrDefault(g => g.Key == ParcelSize.Medium);

        if (mediumParcels != null)
        {
            totalDiscount += ApplyDiscount(mediumParcels, 3);
        }

        totalDiscount += ApplyDiscount(parcelCosts, 5);

        return totalDiscount;
    }

    private static double ApplyDiscount(IEnumerable<ParcelCalculatedCost> parcelCosts, int discountFrequency)
    {
        var saving = 0.0;

        var sortedByCost = parcelCosts.OrderBy(p => p.Cost);

        var eligibleForDiscountedParcel = sortedByCost
            .Where(p => p.IsDiscounted == false)
            .ToArray();

        var freeCount = eligibleForDiscountedParcel.Length / discountFrequency;
        
        var parcelIndex = 0;
        
        for (int i = 0; i < freeCount; i++)
        {
            saving += eligibleForDiscountedParcel[parcelIndex].Cost;
            for (int j = parcelIndex; j < discountFrequency + parcelIndex; j++)
            {
                eligibleForDiscountedParcel[j].IsDiscounted = true;
            }
            parcelIndex += discountFrequency;
        }

        return saving;
    }
}