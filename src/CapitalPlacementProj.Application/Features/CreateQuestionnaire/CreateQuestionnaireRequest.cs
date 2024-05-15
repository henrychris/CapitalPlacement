using CapitalPlacementProj.Domain.Dtos;
using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Features.CreateQuestionnaire
{
    public class CreateQuestionnaireRequest
    {
        public string ProgramName { get; set; } = null!;
        public string ProgramDescription { get; set; } = null!;
        public PersonalInformation PersonalInfo { get; set; } = null!;
        public List<QuestionObjectDto> Questions { get; set; } = [];
    }
}
