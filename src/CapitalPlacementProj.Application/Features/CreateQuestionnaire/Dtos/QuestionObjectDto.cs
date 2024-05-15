namespace CapitalPlacementProj.Application.Features.CreateQuestionnaire.Dtos
{
    public class QuestionObjectDto
    {
        public required string Question { get; set; }
        public required string QuestionType { get; set; }
        public int? MaximumChoices { get; set; }
        public List<string>? Choices { get; set; }
    }
}
