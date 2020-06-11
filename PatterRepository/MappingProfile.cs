using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace PatterRepository
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();
            CreateMap<Account, AccountDto>();
        }
    }
}
