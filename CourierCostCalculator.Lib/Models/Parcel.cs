namespace CourierCostCalculator.Lib.Models;

public record Parcel(double Length, double Width, double Height, double Weight)
{
    public string? Name { get; set; } = string.Empty;

    public double Dimension = Math.Max(Length, Math.Max(Width, Height));
};