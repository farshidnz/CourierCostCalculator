using CourierCostCalculator.Lib.Models;
using Calculator = CourierCostCalculator.Lib.CourierCostMultipleParcelsCalculator;

namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostMultipleParcelsCalculator
{
    [Test]
    public void CalculateCost_SmallParcel_Returns3()
    {
        var parcelName = "Parcel";
        var parcel = new Parcel(5, 5, 5,1){ Name = parcelName};
        var totalCost = Calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(3));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
        });
    }
    
    [Test]
    public void CalculateCost_MediumParcel_Returns16()
    {
        var parcel1 = new Parcel(40, 40, 40,1){ Name = "parcel1"};
        var parcel2 = new Parcel(40, 40, 41,1){ Name = "parcel2"};
        var totalCost = Calculator.CalculateCost(new List<Parcel> { parcel1, parcel2 });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(16));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(8));
        });
    }

    [Test]
    public void CalculateCost_MixParcel_Returns3()
    {
        var parcel1 = new Parcel(5, 5, 5,1){ Name = "parcel1"};
        var parcel2 = new Parcel(40, 40, 40, 1){ Name = "parcel2"};
        var parcel3 = new Parcel(50, 50, 50,1 ){ Name = "parcel3"};
        var totalCost = Calculator.CalculateCost(new List<Parcel> { parcel1, parcel2, parcel3 });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(26));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(3));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(15));
        });
    }
}