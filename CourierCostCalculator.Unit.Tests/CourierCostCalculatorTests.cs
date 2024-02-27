using CourierCostCalculator.Lib;
using CourierCostCalculator.Lib.Models;
namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostMultipleParcelsCalculatorTests
{
    private readonly ICourierCostCalculator _calculator = new Lib.CourierCostCalculator();  
    
    [Test]
    public void CalculateCost_SmallParcel_Returns3()
    {
        var parcel = new Parcel(5, 5, 5, 1);
        var cost = _calculator.CalculateCost(parcel);
        Assert.That(cost, Is.EqualTo(3));
    }

    [Test]
    public void CalculateCost_MediumParcel_Returns8()
    {
        var parcel = new Parcel(40, 40, 40, 1);
        var cost = _calculator.CalculateCost(parcel);
        Assert.That(cost, Is.EqualTo(8));
    }

    [Test]
    public void CalculateCost_LargeParcel_Returns15()
    {
        var parcel = new Parcel(80, 80, 80, 1);
        var cost = _calculator.CalculateCost(parcel);
        Assert.That(cost, Is.EqualTo(15));
    }

    [Test]
    public void CalculateCost_XLParcel_Returns25()
    {
        var parcel = new Parcel(120, 30, 50, 1);
        var cost = _calculator.CalculateCost(parcel);
        Assert.That(cost, Is.EqualTo(25));
    }
}