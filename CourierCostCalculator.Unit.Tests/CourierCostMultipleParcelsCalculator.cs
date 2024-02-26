using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostMultipleParcelsCalculator
{
    [Test]
    public void CalculateCost_SmallParcel_Returns3()
    {
        var parcelName = "Parcel";
        var parcel = new Parcel(5, 5, 5){ Name = parcelName};
        var totalCost = Lib.CourierCostMultipleParcelsCalculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.Count, Is.EqualTo(1));
        });
        Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
        Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
    }
    
    [Test]
    public void CalculateCost_MediumParcel_Returns16()
    {
        var parcel1 = new Parcel(40, 40, 40){ Name = "parcel1"};
        var parcel2 = new Parcel(40, 40, 41){ Name = "parcel2"};
        var totalCost = Lib.CourierCostMultipleParcelsCalculator.CalculateCost(new List<Parcel> { parcel1, parcel2 });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(16));
            Assert.That(totalCost.Parcels.Count, Is.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(8));
        });
    }

    [Test]
    public void CalculateCost_MixParcel_Returns8()
    {
        var parcel1 = new Parcel(5, 5, 5){ Name = "parcel1"};
        var parcel2 = new Parcel(40, 40, 40){ Name = "parcel2"};
        var parcel3 = new Parcel(50, 50, 50){ Name = "parcel3"};
        var totalCost = Lib.CourierCostMultipleParcelsCalculator.CalculateCost(new List<Parcel> { parcel1, parcel2, parcel3 });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(26));
            Assert.That(totalCost.Parcels.Count, Is.EqualTo(3));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(15));
        });
    }
}