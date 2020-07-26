using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.DataModels
{
    public class Skill
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }
    }
}
