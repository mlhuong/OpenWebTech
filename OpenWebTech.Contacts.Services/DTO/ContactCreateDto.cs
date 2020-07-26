using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.Services.DTO
{
    public class ContactCreateDto
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(512)]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string MobilePhoneNumber { get; set; }
    }
}
