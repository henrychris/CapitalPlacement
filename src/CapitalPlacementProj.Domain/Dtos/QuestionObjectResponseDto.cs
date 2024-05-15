namespace CapitalPlacementProj.Domain.Dtos
{
    public class QuestionObjectResponseDto
    {
        public required string Id { get; set; }
        public required string Question { get; set; }
        public required string QuestionType { get; set; }
        public int? MaximumChoices { get; set; }
        public List<string>? Choices { get; set; }
    }
}
