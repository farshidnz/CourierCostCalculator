using Ardalis.SmartEnum;

namespace CourierCostCalculator.Lib.Models;


public abstract class ParcelSize(string name, int value) : SmartEnum<ParcelSize>(name, value)
{
    public static readonly ParcelSize Small = new SmallSize(nameof(Small), 1);
    public static readonly ParcelSize Medium = new MediumSize(nameof(Medium), 2);
    public static readonly ParcelSize Large = new LargeSize(nameof(Large), 3);
    public static readonly ParcelSize ExtraLarge = new ExtraLargeSize(nameof(ExtraLarge), 4);
    public static readonly ParcelSize Heavy = new HeavyParcel(nameof(Heavy), 4);
    
    public abstract double DimensionLimit();
    public abstract double WeightLimit();
    public abstract double Cost();
    
    private sealed class SmallSize(string name, int value) : ParcelSize(name, value)
    {
        public override double DimensionLimit() => 10;

        public override double WeightLimit() => 1;
        public override double Cost() => 3;
    }
    
    private sealed class MediumSize(string name, int value) : ParcelSize(name, value)
    {
        public override double DimensionLimit() => 50;

        public override double WeightLimit() => 3;
        public override double Cost() => 8;
    }
    
    private sealed class LargeSize(string name, int value) : ParcelSize(name, value)
    {
        public override double DimensionLimit() => 100;

        public override double WeightLimit() => 6;
        public override double Cost() => 15;
    }
    
    private sealed class ExtraLargeSize(string name, int value) : ParcelSize(name, value)
    {
        public override double DimensionLimit() => 100;
        public override double WeightLimit() => 10;
        public override double Cost() => 25;
    }
    private sealed class HeavyParcel(string name, int value) : ParcelSize(name, value)
    {
        public override double DimensionLimit() => double.NaN;
        public override double WeightLimit() => 50;
        public override double Cost() => 50;
    }
}