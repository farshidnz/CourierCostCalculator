using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;

namespace CourierCostCalculator.API.Integration.Tests;

public class CalculateCourierParcelPrice(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Successful()
    {
        var client = factory.CreateClient();
        var response = await client.PostAsJsonAsync("/CalculateCourierParcels", new List<Parcel>
        {
            new(5, 5, 5, 1)
        });
        
        var jsonBody = await response.Content.ReadAsStringAsync();
        var formattedJson = string.IsNullOrWhiteSpace(jsonBody)
            ? JToken.Parse("{}")
            : JToken.Parse(jsonBody);
        
        Verify(new { HttpBody = formattedJson, HttpResponse = response });
    }
}