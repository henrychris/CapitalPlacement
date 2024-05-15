namespace CapitalPlacementProj.Domain.Enums
{
    public enum QuestionType
    {
        Paragraph,
        YesNo,
        Dropdown,
        Date,
        Number,
        MultipleChoice
    }

    public static class QuestionTypeExtensions
    {
        public static string ToFriendlyString(this QuestionType me)
        {
            return me switch
            {
                QuestionType.Paragraph => "Paragraph",
                QuestionType.YesNo => "YesNo",
                QuestionType.Dropdown => "Dropdown",
                QuestionType.Date => "Date",
                QuestionType.Number => "Number",
                QuestionType.MultipleChoice => "MultipleChoice",
                _ => "Unknown",
            };
        }
    }
}
