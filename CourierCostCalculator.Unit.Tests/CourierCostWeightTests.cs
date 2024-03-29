﻿using CourierCostCalculator.Lib;
using CourierCostCalculator.Lib.Models;

namespace CourierCostCalculator.Unit.Tests;

[TestFixture]
public class CourierCostWeightTests
{
    private readonly ICourierCostCalculator _calculator = new Lib.CourierCostCalculator();  
    
    [Test]
    public void CalculateCost_SmallParcel_Returns3()
    {
        var parcel = new Parcel(5, 5, 5, 0.5);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(3));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(3));
        });
    }

    [Test]
    public void CalculateCost_MediumParcel_Returns8()
    {
        var parcel = new Parcel(40, 40, 40, 2);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(8));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(8));
        });
    }

    [Test]
    public void CalculateCost_LargeParcel_Returns15()
    {
        var parcel = new Parcel(80, 80, 80, 5);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(15));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(15));
        });
    }

    [Test]
    public void CalculateCost_XLParcel_Returns25()
    {
        var parcel = new Parcel(120, 30, 50, 8);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(25));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(25));
        });
    }

    [Test]
    public void CalculateCost_WithSpeedyShipping_DoublesTotalCost()
    {
        var parcel1 = new Parcel(5, 5, 5, 2);
        var parcel2 = new Parcel(40, 40, 40, 5);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel1, parcel2 }, true);
        
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(34));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(2));
        });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels[0].Cost, Is.EqualTo(5));
            Assert.That(totalCost.Parcels[1].Cost, Is.EqualTo(12));
        });
    }

    [Test]
    public void CalculateCost_SmallParcelOverWeightLimit_ReturnsWithExtraCharge()
    {
        var parcel = new Parcel(5, 5, 5, 2.5);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(6));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(6));
        });
    }
    
    [Test]
    public void CalculateCost_ExtraLargeParcelOverWeightLimit_ReturnsWithExtraCharge()
    {
        var parcel = new Parcel(500, 500, 500, 50);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(50));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(50));
        });
    }
    
    [Test]
    public void CalculateCost_SmallParcelHeavyWeightLimit_ReturnsWithExtraCharge()
    {
        var parcel = new Parcel(2, 2, 2, 55);
        var totalCost = _calculator.CalculateCost(new List<Parcel> { parcel });
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.FinalPrice, Is.EqualTo(55));
            Assert.That(totalCost.Parcels, Has.Count.EqualTo(1));
        });
        
        Assert.Multiple(() =>
        {
            Assert.That(totalCost.Parcels.First().Cost, Is.EqualTo(55));
        });
    }
}