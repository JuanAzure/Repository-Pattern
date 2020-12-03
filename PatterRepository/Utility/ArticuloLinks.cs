using Contracts;
using Entities.DataTransferObjects;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PatterRepository.Utility
{
    public class ArticuloLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<ArticuloDto> _dataShaper;

        public ArticuloLinks(LinkGenerator linkGenerator, IDataShaper<ArticuloDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<ArticuloDto> articuloDto, string fields, int articuloId, HttpContext httpContext)
        {
            var shapedArticulos = ShapeData(articuloDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkdedArticulos(articuloDto, fields, articuloId, httpContext, shapedArticulos);

            return ReturnShapedArticulos(shapedArticulos);
        }

        private List<Entity> ShapeData(IEnumerable<ArticuloDto> articuloDto, string fields) =>
            _dataShaper.ShapeData(articuloDto, fields)
                .Select(e => e.Entity)
                .ToList();

        private bool ShouldGenerateLinks(HttpContext httpContext)
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];

            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedArticulos(List<Entity> shapedArticulos) => new LinkResponse { ShapedEntities = shapedArticulos };

        private LinkResponse ReturnLinkdedArticulos(IEnumerable<ArticuloDto> articuloDto, string fields, int  articuloId, HttpContext httpContext, List<Entity> shapedArticulos)
        {
            var articuloDtoList = articuloDto.ToList();

            for (var index = 0; index < articuloDtoList.Count(); index++)
            {
                var employeeLinks = CreateLinksForArticulo(httpContext, articuloId, articuloDtoList[index].articuloId, fields);
                shapedArticulos[index].Add("Links", employeeLinks);
            }

            var articuloCollection = new LinkCollectionWrapper<Entity>(shapedArticulos);
            var linkedEmployees = CreateLinksForEmployees(httpContext, articuloCollection);

            return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
        }

        private List<Link> CreateLinksForArticulo(HttpContext httpContext, int articuloId, int id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "GetArticuiloForCategoria", values: new { articuloId, id, fields }),
                "self",
                "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteArticuiloForCategoria", values: new { articuloId, id }),
                "delete_articulo",
                "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateArticuiloForCategoria", values: new { articuloId, id }),
                "update_articulo",
                "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyArticuiloForCategoria", values: new { articuloId, id }),
                "partially_update_articulo",
                "PATCH")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForEmployees(HttpContext httpContext, LinkCollectionWrapper<Entity> articulosWrapper)
        {
            articulosWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetArticulosForCategoria", values: new { }),
                    "self",
                    "GET"));

            return articulosWrapper;
        }
    }
}
