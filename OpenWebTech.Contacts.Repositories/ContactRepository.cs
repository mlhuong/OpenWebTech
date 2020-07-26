using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenWebTech.Contacts.DataModels;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Repositories
{
    public class ContactRepository : BaseRepository<Contact, int>, IContactRepository
    {
        public ContactRepository(ContactsDbContext contactsDbContext)
            : base(contactsDbContext)
        {
        }

        public Task<Contact> GetFullContact(int id) 
            => this.ContactsDbContext.Contacts.Include(x => x.Skills)
            .ThenInclude(y => y.Skill)
            .SingleOrDefaultAsync(x => x.Id == id);

        public Task<Contact> GetUserFullContact(IdentityUser user, int id)
            => this.ContactsDbContext.Contacts.Include(x => x.Skills)
            .ThenInclude(y => y.Skill)
            .SingleOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);

        public Task<Contact[]> GetUserContacts(IdentityUser user)
            => this.GetList(x => x.UserId == user.Id);

        public Task<Contact> GetUserContact(IdentityUser user, int contactId)
            => this.ContactsDbContext.Contacts.SingleOrDefaultAsync(x => x.Id == contactId && x.UserId == user.Id);
    }
}