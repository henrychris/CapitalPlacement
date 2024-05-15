using CapitalPlacementProj.Domain.Enums;

namespace CapitalPlacementProj.Domain.Entities
{
    public class QuestionObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string Question { get; set; }
        public int? MaximumChoices { get; set; }
        public List<string>? Choices { get; set; }
        public QuestionType QuestionType { get; set; } = QuestionType.Paragraph;
    }
}
