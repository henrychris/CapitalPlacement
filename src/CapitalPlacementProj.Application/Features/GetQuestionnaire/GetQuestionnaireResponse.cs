using CapitalPlacementProj.Domain.Dtos;
using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Features.GetQuestionnaire
{
    public class GetQuestionnaireResponse
    {
        public required string Id { get; set; }
        public required string ProgramName { get; set; }
        public required string ProgramDescription { get; set; }
        public required PersonalInformation PersonalInformation { get; set; }
        public required List<QuestionObjectResponseDto> Questions { get; set; }
    }
}
