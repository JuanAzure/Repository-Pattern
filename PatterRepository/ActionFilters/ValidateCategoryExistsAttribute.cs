using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace PatterRepository.ActionFilters
{
    public class ValidateCategoryExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILoggerManager _logger;
        public ValidateCategoryExistsAttribute(IRepositoryWrapper repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (int)context.ActionArguments["id"];
            var categoria = await _repository.Categoria.GetCategoriaAsync(id, trackChanges);

            if (categoria == null)
            {
                _logger.LogInfo($"Categoria with id: {id} doesn't exist in the database.");
                context.Result = new BadRequestObjectResult($"Categoria with id: {id} doesn't exist in the database.");
            }
            else
            {
                context.HttpContext.Items.Add("categoria", categoria);
                await next();
            }
        }
    }
}
