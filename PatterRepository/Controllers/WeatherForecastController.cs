using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PatterRepository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        private ILoggerManager _logger;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper repoWrapper)

        {
            _logger = logger;
            _repoWrapper = repoWrapper;

        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [Route("Repository")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInfo("Fetching all Account from the StoraSge");            
            var AccountLists = await _repoWrapper.Owner.GetAllOwnersAsync();
            _logger.LogInfo($"Returning {AccountLists.Count()} Account.");
            return Ok(AccountLists);
        }
    }
}
