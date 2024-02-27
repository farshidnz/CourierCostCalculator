using Microsoft.Extensions.DependencyInjection;

namespace CourierCostCalculator.Lib;

public static class DependencyInjection
{
    public static void AddCourierCostCalculator(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICourierCostCalculator, CourierCostCalculator>();
    }
}