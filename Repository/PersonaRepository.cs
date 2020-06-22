using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PersonaRepository : RepositoryBase<Persona>, IPersonaRepository
    {
        public PersonaRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<IEnumerable<Persona>> GetAllPersonaAsync(bool trackChanges) =>
            await FindAll(trackChanges).OrderBy(p => p.Nombre)
            .ToListAsync();
        public async Task<Persona> GetPersonaAsync(int personaId, bool trackChanges) =>

           await FindByCondition(c => c.Id.Equals(personaId), trackChanges)                  
                .SingleOrDefaultAsync();

        public async Task<IEnumerable<Persona>> GetPersonaByTypeAsync(string tipoPersona, bool trackChanges) =>

            await FindAll(trackChanges)
                  .Where(p => p.TipoPersona == tipoPersona)
                  .ToListAsync();
        public void CreatePersona(Persona persona) => Create(persona);
        public void DeletePersona(Persona persona) => Delete(persona);
    }
}
