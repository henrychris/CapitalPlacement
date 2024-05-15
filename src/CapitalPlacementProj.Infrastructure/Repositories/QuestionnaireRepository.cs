using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Application.Interfaces.Repositories;
using CapitalPlacementProj.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace CapitalPlacementProj.Infrastructure.Repositories
{
    public class QuestionnaireRepository(
        CosmosClient cosmosClient,
        string databaseName,
        string containerName
    ) : IQuestionnaireRepository
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

        public async Task<Questionnaire> CreateQuestionnaireAsync(Questionnaire questionnaire)
        {
            throw new NotImplementedException();
            var response = await _container.CreateItemAsync(questionnaire);
            return response.Resource;
        }

        public async Task<Questionnaire?> GetQuestionnaireAsync(string questionnaireId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateQuestionnaireAsync(Questionnaire questionnaire)
        {
            throw new NotImplementedException();
        }
    }
}
