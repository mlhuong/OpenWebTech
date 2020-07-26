using HMapper;
using OpenWebTech.Contacts.DataModels;
using OpenWebTech.Contacts.Services.DTO;

namespace OpenWebTech.Contacts.Services.Mappings
{
    public static class ServicesMapperConfiguration
    {
        public static void Init(IMapperAPIInitializer initializer)
        {
            initializer.Map<Skill, SkillDto>();
            initializer.Map<Contact, ContactDto>();
            initializer.Map<ContactSkill, ContactSkillDto>();
            
            initializer.Map<ContactUpdateDto, Contact>();
            initializer.Map<ContactCreateDto, Contact>();
            initializer.Map<SkillCreateDto, Skill>();
        }
    }
    
}
