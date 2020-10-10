using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;

namespace PatterRepository
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            #region Mapeo Objeto Categoria
            CreateMap<Categoria, CategoriaDto>()
                .ForMember(x => x.categoriaId, opt => opt.MapFrom(c => c.CategoriaId));
            CreateMap<CategoriaForCreationDto, Categoria>();
            CreateMap<CategoriaForUpdateDto, Categoria>().ReverseMap();
            #endregion

            #region Mapeo Objeto Persona
            CreateMap<Persona, PersonaDto>()
            .ForMember(x => x.personaId, opt => opt.MapFrom(p => p.Id));
            CreateMap<PersonaForCreationDto, Persona>();
            CreateMap<PersonaForUpdateDto, Persona>();
            #endregion
          
            #region Mapeo Objeto Articulo
            CreateMap<Articulo, ArticuloDto>()
                .ForMember(c => c.Categoria, opt => opt.MapFrom(x => x.Categoria.Nombre));
            CreateMap<ArticuloForCreationDto, Articulo>();
            CreateMap<ArticuloForUpdateDto, Articulo>().ReverseMap();

            #endregion
        }
    }
}

