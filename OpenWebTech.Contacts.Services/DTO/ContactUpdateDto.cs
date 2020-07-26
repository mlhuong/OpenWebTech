using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.Services.DTO
{
    public class ContactUpdateDto : ContactCreateDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
