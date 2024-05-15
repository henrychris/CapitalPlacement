namespace CapitalPlacementProj.Domain.Entities
{
    public class PersonalInformation
    {
        public required PersonalInformationObject FirstName { get; set; }
        public required PersonalInformationObject LastName { get; set; }
        public required PersonalInformationObject Email { get; set; }
        public required ProfileInformation Phone { get; set; }
        public required ProfileInformation Nationality { get; set; }
        public required ProfileInformation CurrentResidence { get; set; }
        public required ProfileInformation IdNumber { get; set; }
        public required ProfileInformation DateOfBirth { get; set; }
        public required ProfileInformation Gender { get; set; }
    }

    public class PersonalInformationObject
    {
        public bool IsMandatory { get; set; }
    }

    public class ProfileInformation
    {
        public bool IsInternal { get; set; }
        public bool IsHidden { get; set; }
    }
}
