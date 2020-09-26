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
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace PatterRepository.Controllers
{
    [Route("api/personas")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        #region Propiedades
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PersonaController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> GetAllPersonas()
        {
            var personas = await _repository.Persona.GetAllPersonaAsync(trackChanges: false);
            if (personas == null)
            {
                _logger.LogInfo($"El objecto Categorias no contiene datos. {personas.Count()}");
                return NotFound();
            }
            var personaDto = _mapper.Map<IEnumerable<PersonaDto>>(personas);
            _logger.LogInfo($"Returning {personaDto.Count()} personas.");
            return Ok(personaDto);
        }

        [Route("TipoPersona/{tipoPersona}")]
        [HttpGet]
        public async Task<IActionResult> GetTipoPersonas(string tipoPersona)
        {
            var personas = await _repository.Persona.GetPersonaByTypeAsync(tipoPersona,trackChanges: false);
            if (personas == null)
            {
                _logger.LogInfo($"El objecto PersonaTipo no contiene datos. {personas.Count()}");
                return NotFound();
            }
            var personaDto = _mapper.Map<IEnumerable<PersonaDto>>(personas);
            _logger.LogInfo($"Returning {personaDto.Count()} personas.");
            return Ok(personaDto);
        }

        [HttpGet("{id}", Name = "PersonaId")]
        public async Task<IActionResult> GetPersona(int id)
        {
            var persona = await _repository.Persona.GetPersonaAsync(id, trackChanges: false);
            if (persona == null)
            {
                _logger.LogInfo($"Persona with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var personaDto = _mapper.Map<PersonaDto>(persona);
            _logger.LogInfo($"Returning Controller - Name:{nameof(GetPersona)}");
            return Ok(personaDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersona([FromBody] PersonaForCreationDto _persona)
        {
            if (_persona == null)
            {
                _logger.LogError("PersonaForCreationDto object sent from client is null.");
                return BadRequest("PersonaForCreationDto object is null");
            }

            var personaEntity = _mapper.Map<Persona>(_persona);

            _repository.Persona.CreatePersona(personaEntity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;                
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el  :" + personaEntity.NumDocumento);
            }

            var PersonaToReturn = _mapper.Map<PersonaDto>(personaEntity);
            return CreatedAtRoute("PersonaId", new { id = PersonaToReturn.personaId }, PersonaToReturn);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePersona(int id, [FromBody] PersonaForUpdateDto persona)
        {
            if (persona == null)
            {
                _logger.LogError("PersonaForUpdateDto object sent from client is null.");
                return BadRequest("PersonaForUpdateDto object is null");
            }

            var personaEntity = await _repository.Persona.GetPersonaAsync(id, trackChanges: true);
            if (personaEntity == null)
            {
                _logger.LogInfo($"Persona with id: {id} doesn't exist in the database.");

                return NotFound();
            }   
            _mapper.Map(persona, personaEntity);
            try
            {
                await _repository.SaveAsync();
            }
            catch (DbUpdateException e)
            {
                var error = e.InnerException.Message;
                if (error.Contains("UNIQUE KEY"))
                    _logger.LogError(error);
                return BadRequest("No se puede insertar una clave duplicada en el  :" + personaEntity.NumDocumento);
            }
            
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersona(int id)
        {
            var personaEntity = await _repository.Persona.GetPersonaAsync(id, trackChanges: true);
            if (personaEntity == null)
            {
                _logger.LogInfo($"Persona with id: {id} doesn't exist in the database.");
                return NotFound();
            }
             _repository.Persona.Delete(personaEntity);
            await _repository.SaveAsync();
            return NoContent();
        }

    }
}
