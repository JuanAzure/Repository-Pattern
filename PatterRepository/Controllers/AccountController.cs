using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{

    [Route("api/account")]

    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public AccountController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Account()        {
            var account = await _repository.Account.GetAllAccountsAsync(trackChanges: false);
            _logger.LogInfo($"Returned all owners from database.");
            var ownersResult = _mapper.Map<IEnumerable<AccountDto>>(account);
            _logger.LogInfo($"Returning {ownersResult} Owners.");
            return Ok(ownersResult);
        }

        [HttpGet("{id}",Name ="AccounId") ]
        public async Task<IActionResult> GetAccount(int id)
        {
            var account = await _repository.Account.GetAccountWithDetailsAsync(id,trackChanges: false);
            _logger.LogInfo($"Returned all owners from database.");
            var ownersResult = _mapper.Map<AccountDto>(account);
            _logger.LogInfo($"Returning {ownersResult} Owners.");
            return Ok(ownersResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody]AccountForCreationDto account)
        {
            if (account == null)
            {
                _logger.LogError("AccountForCreationDto object sent from client is null.");
                return BadRequest("AccountForCreationDto object is null");
            }

            var accountEntity = _mapper.Map<Account>(account);
            _repository.Account.CreateAccount(accountEntity);
            await _repository.SaveAsync();

            var consulta = await _repository.Account.GetAccountWithDetailsAsync(accountEntity.Id, trackChanges: false);
            var AccountToReturn = _mapper.Map<AccountDto>(consulta);            

            return CreatedAtRoute("AccounId", new { id= AccountToReturn.Id}, AccountToReturn);



        }
    }
}