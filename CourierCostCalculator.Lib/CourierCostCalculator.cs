using CourierCostCalculator.Lib.Extensions;
using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Lib;

public class CourierCostCalculator : ICourierCostCalculator
{
    public TotalCost CalculateCost(IEnumerable<Parcel> parcels, bool speedyShipping = false)
    {
        var parcelCosts = new List<ParcelCalculatedCost>();
        foreach (var parcel in parcels)
        {
            var cost = CalculateCost(parcel);

            var parcelSize = parcel.GetParcelSize();
            parcelCosts.Add(new ParcelCalculatedCost(parcel.Name ?? string.Empty, cost, parcelSize));
        }

        var discountSaving = parcelCosts.ApplyDiscounts();

        return new TotalCost(parcelCosts)
        {
            SpeedyShipping = speedyShipping,
            TotalDiscount = discountSaving
        };
    }

    public double CalculateCost(Parcel parcel)
    {
        var weightLimit = parcel.GetWeightLimitForParcelSize();
        var cost = parcel.GetBaseCostForParcelSize();

        if (parcel.Weight > weightLimit)
        {
            var extraWeight = parcel.Weight - weightLimit;
            var extraWeightCharge = parcel.Weight >= ParcelSize.Heavy.WeightLimit() ? 1 : 2;
            return cost + extraWeight * extraWeightCharge;
        }

        return cost;
    }
}