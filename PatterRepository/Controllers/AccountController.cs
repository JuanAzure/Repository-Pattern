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

            var AccountToReturn = _mapper.Map<AccountResulDto>(accountEntity);

            return CreatedAtRoute("AccountId", new { id= AccountToReturn.Id}, AccountToReturn);
            //return CreatedAtRoute("accountId", AccountToReturn,null);

            // return Ok(AccountToReturn);


        }
    }
}