using CourierCostCalculator.Lib;
using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostMultipleParcelsCalculatorWithSpeedyShipping
{
    private readonly ICourierCostCalculator _calculator = new Lib.CourierCostCalculator();  
    
    [Test]
    public void CalculateCost_SmallParcel()
    {
        var parcelName = "Parcel";
        var parcel = new Parcel(5, 5, 5,1) { Name = parcelName };
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel }, speedyShipping: true);
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(6));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
        Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
    }

    [Test]
    public void CalculateCost_MediumParcel()
    {
        var parcel1 = new Parcel(40, 40, 40,1) { Name = "parcel1" };
        var parcel2 = new Parcel(40, 40, 41,1) { Name = "parcel2" };
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel1, parcel2 },
                speedyShipping: true);
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(32));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(8));
        });
    }

    [Test]
    public void CalculateCost_MixParcel()
    {
        var parcel1 = new Parcel(5, 5, 5,1) { Name = "parcel1" };
        var parcel2 = new Parcel(40, 40, 40,1) { Name = "parcel2" };
        var parcel3 = new Parcel(50, 50, 50,1) { Name = "parcel3" };
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel1, parcel2, parcel3 },
                speedyShipping: true);
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(52));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(3));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.Last().Cost, Is.EqualTo(15));
        });
    }
}