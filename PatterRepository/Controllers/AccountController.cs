using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using PatterRepository.ModelBinders;

namespace PatterRepository.Controllers
{
    [Route("api/accounts")]
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

        [Route("{id}/owners/{ownerId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAccountForOwner(int ownerId, int id)
        {
            var owner = await _repository.Owner.GetOwnerByIdAsync(ownerId, trackChanges: false);
            if (owner == null)
            {
                _logger.LogInfo($"Owner with id: {ownerId} doesn't exist in the database.");
                return NotFound();
            }

            var accountForOwner = await _repository.Account.GetAccountAsync(ownerId, id, trackChanges: false);
            if (accountForOwner == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _repository.Account.DeleteAccount(accountForOwner);
            await _repository.SaveAsync();

            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAccount()
        {
            var account = await _repository.Account.GetAllAccountsAsync(trackChanges: false);
            _logger.LogInfo($"Returned all Accounts from database.");
            var ownersResult = _mapper.Map<IEnumerable<AccountDto>>(account);
            _logger.LogInfo($"Returning {ownersResult.Count()} Accounts.");
            _logger.LogError($"Controlador {nameof(GetAllAccount)}");

            return Ok(ownersResult);
        }

        [HttpGet("{id}", Name = "AccounId")]
        public async Task<IActionResult> GetByIdAccount(int id)
        {
            var account = await _repository.Account.GetAccountWithDetailsAsync(id, trackChanges: false);
            if (account == null)
            {
                _logger.LogInfo($"Accoun with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _logger.LogInfo($"Returned all owners from database.");
            var ownersResult = _mapper.Map<AccountDto>(account);
            _logger.LogInfo($"Returning {ownersResult} Owners.");
            return Ok(ownersResult);
        }

        [HttpGet("{id}/owners/{idOwner}")]
        public async Task<IActionResult> GetAccountOwner(int idOwner, int id)
        {
            var owner = await _repository.Owner.GetOwnerByIdAsync(idOwner, trackChanges: false);

            if (owner == null)
            {
                _logger.LogInfo($"Owner with id: {idOwner} doesn't exist in the database.");
                return NotFound();
            }

            var account = await _repository.Account.GetAccountAsync(idOwner, id, trackChanges: false);

            if (account == null)
            {
                _logger.LogInfo($"Account with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            _logger.LogInfo($"Returned all owners from database.");
            var accountResult = _mapper.Map<AccountDto>(account);
            _logger.LogInfo($"Returning {accountResult} AccountOwner.");
            return Ok(accountResult);
        }

        [Route("{ownerId}/owners")]
        [HttpPost]
        public async Task<IActionResult> CreateAccount(int ownerId, [FromBody] AccountForCreationDto account)
        {
            if (account == null)
            {
                _logger.LogError("AccountForCreationDto object sent from client is null.");
                return BadRequest("AccountForCreationDto object is null");
            }

            var accountEntity = _mapper.Map<Account>(account);
            _repository.Account.CreateAccountForOwner(ownerId, accountEntity);
            await _repository.SaveAsync();

            var consulta = await _repository.Account.GetAccountWithDetailsAsync(accountEntity.Id, trackChanges: false);
            var AccountToReturn = _mapper.Map<AccountDto>(consulta);

            return CreatedAtRoute("AccounId", new { ownerId, id = AccountToReturn.Id }, AccountToReturn);
        }

        [HttpGet("collection/({ids})", Name = "OwnerCollection")]
        public async Task<IActionResult> GetOwnersCollection
            ([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<int> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }

            var ownersEntities = await _repository.Owner.GetByIds(ids, trackChanges: false);

            if (ids.Count() != ownersEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var companiesToReturn = _mapper.Map<IEnumerable<OwnerDto>>(ownersEntities);
            return Ok(companiesToReturn);
        }


        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody]
          IEnumerable<OwnerForCreationDto> companyCollection)
        {
            if (companyCollection == null)
            {
                _logger.LogError("Company collection sent from client is null.");
                return BadRequest("Company collection is null");
            }
            var ownerEntities = _mapper.Map<IEnumerable<Owner>>(companyCollection);

            foreach (var owner in ownerEntities)
            {
                _repository.Owner.Create(owner);
            }
            await _repository.SaveAsync();
            var companyCollectionToReturn =
           _mapper.Map<IEnumerable<OwnerDto>>(ownerEntities);
            var ids = string.Join(",", companyCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("OwnerCollection", new { ids },
           companyCollectionToReturn);
        }
    }


}