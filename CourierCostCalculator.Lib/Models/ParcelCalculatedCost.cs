namespace CourierCostCalculator.Lib.Models;

public record ParcelCalculatedCost(string Name, double Cost, ParcelSize Size)
{
    public bool IsDiscounted { get; set; } = false;
}