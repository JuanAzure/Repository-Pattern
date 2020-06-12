using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace PatterRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();
            CreateMap<Account, AccountDto>()
             .ForMember(c => c.Owner, opt => opt.MapFrom(x => x.owner.Name));

            CreateMap<AccountForCreationDto, Account>();
            CreateMap<Account, AccountResulDto>();




        }
    }
}

