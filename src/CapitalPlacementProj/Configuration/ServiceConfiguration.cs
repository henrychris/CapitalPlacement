using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Infrastructure.Repositories;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CapitalPlacementProj.Configuration;

public static class ServiceConfiguration
{
    /// <summary>
    /// Register services in the DI container.
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IQuestionnaireRepository>(provider =>
        {
            var cosmosClient = provider.GetRequiredService<CosmosClient>();
            return new QuestionnaireRepository(cosmosClient, "cosmic_works", "questionnaires");
        });

        // used for time manipulation and testing
        // we should use this instead of DateTime.Now
        services.AddSingleton(TimeProvider.System);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
