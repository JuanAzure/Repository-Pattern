using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace PatterRepository.ActionFilters
{
    public class ValidateArticuloExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        public ValidateArticuloExistsAttribute(IRepositoryWrapper repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var categoriaId = (int)context.ActionArguments["categoriaId"];
            var categoria = await _repository.Categoria.GetCategoriaAsync(categoriaId, false);

            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {categoriaId} doesn't exist in the database.");
                context.Result = new BadRequestObjectResult($"Categoria with id: { categoriaId } doesn't exist in the database.");
                return;
            }

            var id = (int)context.ActionArguments["id"];
            var Articulo = await _repository.Articulo.GetArticuloCategoriaAsync(categoriaId, id, trackChanges);

            if (Articulo == null)
            {
                _logger.LogInfo($"Articulo with id: {id} doesn't exist in the database.");
                context.Result = new BadRequestObjectResult($"Articulo with id: {id} doesn't exist in the database.");
            }
            else
            {
                context.HttpContext.Items.Add("articulo", Articulo);
                await next();
            }
        }
    }
}
