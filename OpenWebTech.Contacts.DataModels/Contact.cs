using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace OpenWebTech.Contacts.DataModels
{
    public class Contact
    {
        public Contact()
        {
            Skills = new Collection<ContactSkill>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [MaxLength(512)]
        public string Address { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string MobilePhoneNumber { get; set; }

        public ICollection<ContactSkill> Skills { get; set; }
    }
}
