using CapitalPlacementProj.Application.Interfaces;
using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Infrastructure.Repositories;
using CapitalPlacementProj.Infrastructure.Services;
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
        services.AddScoped<IQuestionnaireService, QuestionnaireService>();

        services.AddScoped<IQuestionnaireRepository>(provider =>
        {
            var cosmosClient = provider.GetRequiredService<CosmosClient>();
            return new QuestionnaireRepository(cosmosClient, "cosmic_works", "questionnaires");
        });

        services.AddScoped<IQuestionnaireResponseRepository>(provider =>
        {
            var cosmosClient = provider.GetRequiredService<CosmosClient>();
            return new QuestionnaireResponseRepository(
                cosmosClient,
                "cosmic_works",
                "questionnaire_responses"
            );
        });

        // used for time manipulation and testing
        // we should use this instead of DateTime.Now
        services.AddSingleton(TimeProvider.System);
        services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}
