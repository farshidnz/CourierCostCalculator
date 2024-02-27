using AutoMapper;
using AutoMapper.Internal;
using CourierCostCalculator.API.Mappings;
using CourierCostCalculator.Lib;
using Microsoft.AspNetCore.Mvc;

namespace CourierCostCalculator.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAutoMapper(expression => { expression.Internal().MethodMappingEnabled = false; },
            typeof(ParcelMapping).Assembly);
        
        
        builder.Services.AddCourierCostCalculator();

        var app = builder.Build();

        app.UseHttpsRedirection();

        var mapper = app.Services.GetService<IMapper>();
        var courierCalculator = app.Services.GetService<ICourierCostCalculator>();

        app.MapPost("/CalculateCourierParcels", ([FromBody] List<Parcel> parcels) =>
            {
                var mappedParcel = mapper.Map<List<CourierCostCalculator.Lib.Models.Parcel>>(parcels);

                return courierCalculator.CalculateCost(mappedParcel);
            })
            .WithName("CalculateCourierParcelsPrice")
            .WithOpenApi();

        app.Run();
    }
}

public record Parcel(double Length, double Width, double Height, double Weight)
{
    public string? Name { get; set; } = string.Empty;
};