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
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public async Task<IEnumerable<Owner>> GetAllOwnersAsync()
        {
            return await FindAll(trackChanges:false).OrderBy(ow => ow.Name).ToListAsync();
        }

        public async Task<Owner> GetOwnerByIdAsync(int ownerId)
        {
            return await FindCondition(owner => owner.Id.Equals(ownerId))
                .FirstOrDefaultAsync();
        }
        public async Task<Owner> GetOwnerWithDetailsAsync(int ownerId)
        {
            return await FindCondition(owner => owner.Id.Equals(ownerId))
                .Include(ac => ac.Accounts).FirstOrDefaultAsync();
        }

        public void CreateOwner(Owner owner)
        {
            Create(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            Update(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            Delete(owner);
        }
    }
}
