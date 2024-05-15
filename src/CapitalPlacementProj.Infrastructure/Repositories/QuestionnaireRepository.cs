using CapitalPlacementProj.Application.Interfaces.Repositories;
﻿using System.Net;
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
            try
            {
                var response = await _container.ReadItemAsync<Questionnaire>(
                    questionnaireId,
                    new PartitionKey(questionnaireId)
                );
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateQuestionnaireAsync(Questionnaire questionnaire)
        {
            throw new NotImplementedException();
        }
    }
}
