using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Mvc;

namespace PatterRepository.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            var owner = await _repository.Owner.GetAllOwnersAsync();
            _logger.LogInfo($"Returned all owners from database.");

            var ownersResult = _mapper.Map<IEnumerable<OwnerDto>>(owner);
            _logger.LogInfo($"Returning {ownersResult.Count()} Owners.");
            return Ok(ownersResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            _logger.LogInfo($"Returned owner with id: {id}");
            var owner = await _repository.Owner.GetOwnerByIdAsync(id, trackChanges: false);
            if (owner == null)
            {
                _logger.LogInfo($"Owner with id: {id} doesn't exist in the database.");
                _logger.LogError($"Something went wrong in the {nameof(GetOwnerById)}");
                return NotFound();
            }

            var ownerResult = _mapper.Map<OwnerDto>(owner);
            _logger.LogInfo($"Returning {ownerResult} owner.");

            return Ok(ownerResult);
        }

        [HttpGet("{id}/accounts", Name = "OwnerId")]
        public async Task<IActionResult> GetOwnerWithDetails(int id)
        {
            _logger.LogInfo($"Returned owner with id: {id}");
            var owner = await _repository.Owner.GetOwnerWithDetailsAsync(id, trackChanges: false);

            var ownerResult = _mapper.Map<OwnerDto>(owner);
            _logger.LogInfo($"Returning ,{ownerResult} owner/account.");
            return Ok(ownerResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] OwnerForCreationDto companyEntity)
        {
            if (companyEntity == null)
            {
                _logger.LogError("Company collection sent from client is null.");
                return BadRequest("Company collection is null");
            }

            var ownerEntities = _mapper.Map<Owner>(companyEntity);
            _repository.Owner.Create(ownerEntities);
            await _repository.SaveAsync();

            var AccountToReturn = _mapper.Map<OwnerDto>(ownerEntities);
            return CreatedAtRoute("OwnerId", new { id = AccountToReturn.Id }, AccountToReturn);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _repository.Owner.GetOwnerByIdAsync(id, trackChanges: false);
            if (owner == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Owner.DeleteOwner(owner);
            await _repository.SaveAsync();
            return NoContent();
        }



        [Route("{id}/accounts/{accountId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateOwner(int accountId, int id, [FromBody] OwnerForUpdateDto owner)
        {
            if (owner == null)
            {
                _logger.LogError("ownewrForUpdateDto object sent from client is null.");
                return BadRequest("EmployeeForUpdateDto object is null");
            }

            var account = await _repository.Account.GetAccountWithDetailsAsync(accountId, trackChanges: false);
            if (account == null)
            {
                _logger.LogError($"Account object:  {accountId} doesn't exist in the database.");
                return NotFound();
            }

            var ownerEntity = await _repository.Owner.GetOwnerWithDetailsAsync(id, trackChanges: true);
            if (ownerEntity==null)
            {
                _logger.LogError($"Owner object:  {id}doesn't exist in the database.");
                return NotFound();
            }

            _mapper.Map(owner, ownerEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}



