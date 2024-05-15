using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Interfaces.Repositories
{
    public interface IQuestionnaireRepository
    {
        Task InitializeAsync();
        Task<Questionnaire> CreateQuestionnaireAsync(Questionnaire questionnaire);
        Task<Questionnaire?> GetQuestionnaireAsync(string questionnaireId);
        Task UpdateQuestionnaireAsync(Questionnaire questionnaire);
    }
}
