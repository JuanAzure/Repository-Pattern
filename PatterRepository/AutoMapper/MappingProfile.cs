using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace PatterRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<AccountForCreationDto, Account>();
            CreateMap<Account, AccountDto>()
             .ForMember(c => c.Owner, opt => opt.MapFrom(x => x.owner.Name));

            CreateMap<Owner, OwnerDto>();
            CreateMap<OwnerForCreationDto, Owner>();
            CreateMap<OwnerForUpdateDto, Owner>();

            CreateMap<Articulo, ArticuloDto>()
                .ForMember(c => c.Categoria, opt => opt.MapFrom(x => x.Categoria.Nombre));

            CreateMap<ArticuloForCreationDto, Articulo>();

        }
    }
}

