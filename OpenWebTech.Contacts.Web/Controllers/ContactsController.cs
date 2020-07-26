using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenWebTech.Contacts.DataModels;
using OpenWebTech.Contacts.Services;
using OpenWebTech.Contacts.Services.DTO;
using OpenWebTech.Contacts.Services.Exceptions;
using OpenWebTech.Contacts.Web.Security;

namespace OpenWebTech.Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [JwtAuthorize]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService contactService;

        public ContactsController(ContactService contactService)
        {
            this.contactService = contactService ?? throw new ArgumentNullException(nameof(contactService));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetUserContacts()
        {
            ContactDto[] contacts = await this.contactService.GetList(this.User);
            return this.Ok(contacts);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateContact(ContactCreateDto contactInput)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            ContactDto contact = await this.contactService.CreateContact(this.User, contactInput);
            return this.Ok(contact);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContact(int id)
        {
            ContactDto contact = await this.contactService.GetContact(id);
            return this.Ok(contact);   
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateContact(ContactUpdateDto contactInput)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.contactService.UpdateContact(this.User, contactInput);
            return this.Ok();
            
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await this.contactService.DeleteContact(this.User, id);
            return this.Ok();
        }

        [HttpPut("{id}/skill")]
        public async Task<IActionResult> AddOrUpdateSkill(int id, int skillId, SkillLevel level)
        {
            await this.contactService.AddOrUpdateSkill(this.User, id, skillId, level);
            return this.Ok();
        }

        [HttpDelete("{id}/skill")]
        public async Task<IActionResult> DeleteSkill(int id, int skillId)
        {
            await this.contactService.DeleteSkill(this.User, id, skillId);
            return this.Ok();
        }
    }
}
