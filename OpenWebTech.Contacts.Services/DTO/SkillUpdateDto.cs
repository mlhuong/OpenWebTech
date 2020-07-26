using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.Services.DTO
{
    public class SkillUpdateDto : SkillCreateDto
    {
        [Required]
        public int? Id { get; set; }
    }
}
