namespace CapitalPlacementProj.Domain.Entities
{
    public class QuestionnaireResponse
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string QuestionnaireId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string UserId { get; set; }
        public required string? Phone { get; set; }
        public required string? Nationality { get; set; }
        public required string? CurrentResidence { get; set; }
        public required string? IdNumber { get; set; }
        public required DateTime? DateOfBirth { get; set; }
        public required string? Gender { get; set; }
        public required List<Response> Responses { get; set; }
    }

    public class Response
    {
        public required string QuestionId { get; set; }
        public required object Answer { get; set; }
    }
}
