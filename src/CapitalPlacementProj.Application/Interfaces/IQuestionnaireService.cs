using CapitalPlacementProj.Application.Features.CreateQuestionnaire;
using CapitalPlacementProj.Application.Features.GetQuestionnaire;
using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Interfaces
{
    public interface IQuestionnaireService
    {
        Task InitializeAsync();
        Task<Questionnaire?> CreateQuestionnaireAsync(CreateQuestionnaireRequest request);
        Task<GetQuestionnaireResponse?> GetQuestionnaireAsync(string questionnaireId);
    }
}
