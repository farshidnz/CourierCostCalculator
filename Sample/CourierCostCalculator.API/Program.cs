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

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(expression => { expression.Internal().MethodMappingEnabled = false; },
            typeof(ParcelMapping).Assembly);

        var app = builder.Build();


// Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var mapper = app.Services.GetService<IMapper>();

        app.MapPost("/CalculateCourierParcels", ([FromBody] List<Parcel> parcels) =>
            {
                var mappedParcel = mapper.Map<List<CourierCostCalculator.Lib.Models.Parcel>>(parcels);

                return CourierCostMultipleParcelsCalculator.CalculateCost(mappedParcel);
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