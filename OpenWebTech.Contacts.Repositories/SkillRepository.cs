using OpenWebTech.Contacts.DataModels;
using System;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Repositories
{
    public class SkillRepository : BaseRepository<Skill, int>, ISkillRepository
    {
        public SkillRepository(ContactsDbContext contactsDbContext)
            : base(contactsDbContext)
        {
        }
    }
}