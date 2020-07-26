using Microsoft.AspNetCore.Identity;
using OpenWebTech.Contacts.DataModels;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact, int>
    {
        Task<Contact> GetFullContact(int id);

        Task<Contact> GetUserFullContact(IdentityUser user, int id);

        Task<Contact[]> GetUserContacts(IdentityUser user);

        Task<Contact> GetUserContact(IdentityUser user, int contactId);
    }
}
