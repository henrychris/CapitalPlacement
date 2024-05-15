using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Interfaces.Repositories
{
    public interface IQuestionnaireResponseRepository
    {
        Task InitializeAsync();
        Task SaveQuestionnaireResponseAsync(QuestionnaireResponse questionnaireResponse);
    }
}
