using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace OpenWebTech.Contacts.DataModels
{
    public class ContactsDbContext : IdentityDbContext
    {
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactSkill>().HasKey(nameof(ContactSkill.ContactId), nameof(ContactSkill.SkillId));
        }
    }
}
