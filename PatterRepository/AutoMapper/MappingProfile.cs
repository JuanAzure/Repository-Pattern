using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Globalization;

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


            #region Mapeo Objeto Articulo

            CreateMap<Articulo, ArticuloDto>()
                .ForMember(c => c.Categoria, opt => opt.MapFrom(x => x.Categoria.Nombre));
            CreateMap<ArticuloForCreationDto, Articulo>();
            CreateMap<ArticuloForUpdateDto, Articulo>();

            #endregion

            #region Mapeo Objeto Categoria
            CreateMap<Categoria, CategoriaDto>();           
            CreateMap<CategoriaForCreationDto, Categoria>();
            CreateMap<CategoriaForUpdateDto, Categoria>();

            #endregion

        }
    }
}

