using CapitalPlacementProj.Domain.Entities;

namespace CapitalPlacementProj.Application.Features.AnswerQuestionnaire
{
    public class AnswerQuestionnaireRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? IdNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<Response> Responses { get; set; } = [];
    }
}
