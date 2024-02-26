namespace CourierCostCalculator.Lib.Models;

public record Parcel(double Length, double Width, double Height)
{
    public string? Name { get; set; } = string.Empty;
};