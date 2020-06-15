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

        public async Task<Owner> GetOwnerByIdAsync(int ownerId,bool trackChanges)=>
        
             await FindCondition(owner => owner.Id.Equals(ownerId),trackChanges).FirstOrDefaultAsync();

        public async Task<Owner> GetOwnerWithDetailsAsync(int ownerId,bool trackChanges)=>
        
             await FindCondition(owner => owner.Id.Equals(ownerId),trackChanges)
                                                  .Include(ac => ac.Accounts).FirstOrDefaultAsync();

        public async Task<IEnumerable<Owner>> GetByIds(IEnumerable<int> ids, bool trackChanges)
             => await FindCondition(x=> ids.Contains(x.Id),trackChanges).ToListAsync();
       
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
