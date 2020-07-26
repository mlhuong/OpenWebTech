using HMapper;
using Microsoft.AspNetCore.Identity;
using OpenWebTech.Contacts.DataModels;
using OpenWebTech.Contacts.Repositories;
using OpenWebTech.Contacts.Services.DTO;
using OpenWebTech.Contacts.Services.Exceptions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Services
{
    public class ContactService
    {
        private readonly IContactRepository contactRepository;
        private readonly ISkillRepository skillRepository;
        private readonly UserManager<IdentityUser> userManager;

        public ContactService(IContactRepository contactRepository, ISkillRepository skillRepository, UserManager<IdentityUser> userManager)
        {
            this.contactRepository = contactRepository ?? throw new ArgumentNullException(nameof(contactRepository));
            this.skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ContactDto[]> GetList(ClaimsPrincipal principal)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact[] contacts = await this.contactRepository.GetUserContacts(user);
            return contacts.Select(Mapper.Map<Contact, ContactDto>).ToArray();
        }

        public async Task<ContactDto> CreateContact(ClaimsPrincipal principal, ContactCreateDto contactInput)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact contact = Mapper.Map<ContactCreateDto, Contact>(contactInput);
            contact.User = user;
            await this.contactRepository.Add(contact);
            return Mapper.Map<Contact, ContactDto>(contact);
        }

        public async Task<ContactDto> GetContact(int id)
        {
            Contact contact = await this.contactRepository.GetFullContact(id);
            if (contact == null)
            {
                throw new NotFoundException();
            }

            return Mapper.Map<Contact, ContactDto>(contact);
        }

        public async Task UpdateContact(ClaimsPrincipal principal, ContactUpdateDto contactInput)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact contact = await this.contactRepository.GetUserContact(user, contactInput.Id??0);
            if (contact == null)
            {
                throw new NotFoundException();
            }

            Mapper.Fill(contactInput, contact);
            await this.contactRepository.Update(contact);
        }

        public async Task DeleteContact(ClaimsPrincipal principal, int id)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact contact = await this.contactRepository.GetUserContact(user, id);
            if (contact == null)
            {
                throw new NotFoundException();
            }

            await this.contactRepository.Delete(contact);
        }

        public async Task AddOrUpdateSkill(ClaimsPrincipal principal, int contactId, int skillId, SkillLevel level)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact contact = await this.contactRepository.GetUserFullContact(user, contactId);
            if (contact == null)
            {
                throw new NotFoundException();
            }

            Skill skill = await this.skillRepository.Get(skillId);
            if (skill == null)
            {
                throw new NotFoundException();
            }

            ContactSkill contactSkill = contact.Skills.SingleOrDefault(x => x.SkillId == skillId);
            if (contactSkill == null)
            {
                contactSkill = new ContactSkill() { ContactId = contactId, SkillId = skillId };
                contact.Skills.Add(contactSkill);
            }

            contactSkill.Level = level;
            await this.contactRepository.Update(contact);
        }

        public async Task DeleteSkill(ClaimsPrincipal principal, int contactId, int skillId)
        {
            IdentityUser user = await this.userManager.GetUserAsync(principal);
            Contact contact = await this.contactRepository.GetUserFullContact(user, contactId);
            if (contact == null)
            {
                throw new NotFoundException();
            }

            Skill skill = await this.skillRepository.Get(skillId);
            if (skill == null)
            {
                throw new NotFoundException();
            }

            ContactSkill contactSkill = contact.Skills.SingleOrDefault(x => x.SkillId == skillId);
            if (contactSkill == null)
            {
                throw new NotFoundException();
            }

            contact.Skills.Remove(contactSkill);
            await this.contactRepository.Update(contact);
        }
    }
}
