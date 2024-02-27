using CourierCostCalculator.Lib.Models;
using Calculator = CourierCostCalculator.Lib.CourierCostMultipleParcelsCalculator;

namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostCalculatorDiscountTests
{
    [Test]
    public void CalculateCost_MediumParcel_TwoDiscounts()
    {
        var parcelName = "Parcel";
        var parcel1 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel2 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel3 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel4 = new Parcel(40, 40, 40,4){ Name = parcelName};
        var parcel5 = new Parcel(40, 40, 40,4){ Name = parcelName};
        var parcel6 = new Parcel(40, 40, 40,4){ Name = parcelName};
        var totalCost = Calculator.CalculateCost(new List<Parcel>
        {
            parcel1, parcel2, parcel3, parcel4, parcel5, parcel6
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(36));
            Assert.That(totalCost.TotalDiscount, Is.EqualTo(18));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(6));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
            Assert.That(totalCost.Parcels.First().IsDiscounted, Is.True);
        });
    }
    [Test]
    public void CalculateCost_MediumParcel_OneDiscount()
    {
        var parcelName = "Parcel";
        var parcel1 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel2 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel3 = new Parcel(40, 40, 40,1){ Name = parcelName};
        var parcel4 = new Parcel(40, 40, 40,4){ Name = parcelName};
        var parcel5 = new Parcel(40, 40, 40,4){ Name = parcelName};
        var totalCost = Calculator.CalculateCost(new List<Parcel>
        {
            parcel1, parcel2, parcel3, parcel4, parcel5
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(36));
            Assert.That(totalCost.TotalDiscount, Is.EqualTo(8));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(5));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
            Assert.That(totalCost.Parcels.First().IsDiscounted, Is.True);
            Assert.That(totalCost.Parcels.Last().IsDiscounted, Is.False);
        });
    }
    
    [Test]
    public void CalculateCost_SmallParcel_OneDiscount()
    {
        var parcelName = "Parcel";
        var parcel1 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel2 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel3 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel4 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel5 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var totalCost = Calculator.CalculateCost(new List<Parcel>
        {
            parcel1, parcel2, parcel3, parcel4, parcel5
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(12));
            Assert.That(totalCost.TotalDiscount, Is.EqualTo(3));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(5));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.First().IsDiscounted, Is.True);
            Assert.That(totalCost.Parcels.Last().IsDiscounted, Is.False);
        });
    }
    
    [Test]
    public void CalculateCost_SmallParcel_TwoDiscounts()
    {
        var parcelName = "Parcel";
        var parcel1 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel2 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel3 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel4 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel5 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel6 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel7 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var parcel8 = new Parcel(5, 5, 5,1){ Name = parcelName};
        var totalCost = Calculator.CalculateCost(new List<Parcel>
        {
            parcel1, parcel2, parcel3, parcel4, parcel5, parcel6, parcel7, parcel8
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(18));
            Assert.That(totalCost.TotalDiscount, Is.EqualTo(6));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(8));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Name, Is.EqualTo(parcelName));
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
            Assert.That(totalCost.Parcels.First().IsDiscounted, Is.True);
            Assert.That(totalCost.Parcels.Last().IsDiscounted, Is.True);
        });
    }
}