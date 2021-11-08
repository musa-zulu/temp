using AccountManagement.DB.Domain;
using AccountManagement.DB.Dtos;
using AutoMapper;

namespace AccountManagement.Infrastructure.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<CreateOrUpdatePersonDto, Person>()
                .ForMember(dest => dest.Accounts, opt =>
                    opt.Ignore());

        }
    }
}
