namespace CapitalPlacementProj.Domain.Entities
{
    public class Questionnaire
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string ProgramName { get; set; }
        public required string ProgramDescription { get; set; }
        public required PersonalInformation PersonalInformation { get; set; }
        public required List<QuestionObject> Questions { get; set; }
    }
}
