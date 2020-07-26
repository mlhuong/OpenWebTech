using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OpenWebTech.Contacts.Services;
using OpenWebTech.Contacts.Services.DTO;
using OpenWebTech.Contacts.Services.Exceptions;

namespace OpenWebTech.Contacts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly SkillService skillService;

        public SkillsController(SkillService skillService)
        {
            this.skillService = skillService ?? throw new ArgumentNullException(nameof(skillService));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetList()
        {
            SkillDto[] skills = await this.skillService.GetSkills();
            return this.Ok(skills);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateSkill(SkillCreateDto skillInput)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            SkillDto skill = await this.skillService.CreateSKill(skillInput);
            return this.Ok(skill);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSkill(int id)
        {
            SkillDto skill = await this.skillService.GetSkill(id);
            return this.Ok(skill);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSkill(SkillUpdateDto skillInput)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest();
            }

            await this.skillService.UpdateSkill(skillInput);
            return this.Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            await this.skillService.DeleteSkill(id);
            return this.Ok();
        }
    }
}
