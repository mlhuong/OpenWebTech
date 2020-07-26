namespace OpenWebTech.Contacts.Services.DTO
{
    public class ContactDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string MobilePhoneNumber { get; set; }

        public ContactSkillDto[] Skills { get; set; }
    }
}
