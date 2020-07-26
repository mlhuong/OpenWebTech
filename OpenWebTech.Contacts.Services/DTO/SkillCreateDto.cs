using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.Services.DTO
{
    public class SkillCreateDto
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
