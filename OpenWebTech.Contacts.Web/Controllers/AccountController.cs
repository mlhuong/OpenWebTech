using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenWebTech.Contacts.Services;
using OpenWebTech.Contacts.Services.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Web.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly AccountService accountService;

        public AccountController(AccountService accountService)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            if (ModelState.IsValid)
            {
                (string token, string errors) result = await this.accountService.Register(register);
                if (result.errors != null)
                {
                    return this.BadRequest(result.errors);
                }

                return this.Ok(result.token);
            }

            return this.BadRequest();
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Token(string userName, string password)
        {
            string token = await this.accountService.GetToken(userName, password);
            if (token == null)
            {
                return this.Unauthorized();
            }
            return this.Ok(token);
        }

    }
}
