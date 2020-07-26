using HMapper;
using OpenWebTech.Contacts.DataModels;
using OpenWebTech.Contacts.Repositories;
using OpenWebTech.Contacts.Services.DTO;
using OpenWebTech.Contacts.Services.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OpenWebTech.Contacts.Services
{
    public class SkillService
    {
        private readonly ISkillRepository skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            this.skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        }

        public async Task<SkillDto> CreateSKill(SkillCreateDto skillInput)
        {
            Skill skill = Mapper.Map<SkillCreateDto, Skill>(skillInput);
            if (await this.skillRepository.Any(x => x.Name.ToLower() == skill.Name.ToLower()))
            {
                throw new DuplicateException();
            }

            await this.skillRepository.Add(skill);
            return Mapper.Map<Skill, SkillDto>(skill);
        }

        public async Task<SkillDto[]> GetSkills()
        {
            Skill[] skills = await this.skillRepository.GetList(null);
            return skills.Select(Mapper.Map<Skill, SkillDto>).ToArray();
        }

        public async Task<SkillDto> GetSkill(int id)
        {
            Skill skill = await this.skillRepository.Get(id);
            if (skill == null)
            {
                throw new NotFoundException();
            }

            return Mapper.Map<Skill, SkillDto>(skill);
        }

        public async Task UpdateSkill(SkillUpdateDto skillInput)
        {
            Skill skill = await this.skillRepository.Get(skillInput?.Id ?? 0);
            if (skill == null)
            {
                throw new NotFoundException();
            }

            if (!skill.Name.Equals(skillInput.Name, StringComparison.InvariantCultureIgnoreCase)
                && await this.skillRepository.Any(x => x.Id != skillInput.Id && x.Name.ToLower() == skillInput.Name.ToLower()))
            {
                throw new DuplicateException();
            }

            Mapper.Fill(skillInput, skill);
            await this.skillRepository.Update(skill);
        }

        public async Task DeleteSkill(int id)
        {
            Skill skill = await this.skillRepository.Get(id);
            if (skill == null)
            {
                throw new NotFoundException();
            }

            await this.skillRepository.Delete(skill);
        }
    }
}
