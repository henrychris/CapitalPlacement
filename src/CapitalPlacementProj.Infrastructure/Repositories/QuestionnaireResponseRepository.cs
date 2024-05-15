using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacementProj.Infrastructure.Repositories
{
    public class QuestionnaireResponseRepository(
        CosmosClient cosmosClient,
        string databaseName,
        string containerName
    ) : IQuestionnaireResponseRepository
    {
        private Container _container = cosmosClient.GetContainer(databaseName, containerName);

        public async Task InitializeAsync()
        {
            var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseName);
            var containerResponse = await database.Database.CreateContainerIfNotExistsAsync(
                containerName,
                "/id"
            );

            _container = containerResponse.Container;
        }

        public async Task SaveQuestionnaireResponseAsync(
            QuestionnaireResponse questionnaireResponse
        )
        {
            await _container.UpsertItemAsync(questionnaireResponse);
        }
    }
}
