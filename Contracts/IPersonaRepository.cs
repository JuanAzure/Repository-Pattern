using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IPersonaRepository : IRepositoryBase<Persona>
    {
        Task<IEnumerable<Persona>> GetAllPersonaAsync(bool trackChanges);
        Task<Persona> GetPersonaAsync(int personaId, bool trackChanges);
        Task<IEnumerable<Persona>> GetPersonaByTypeAsync(string tipoPersona, bool trackChanges);
        void CreatePersona(Persona persona);
        void DeletePersona(Persona persona);
    }
}
