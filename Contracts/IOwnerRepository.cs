using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
  public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        Task<IEnumerable<Owner>> GetAllOwnersAsync();
        Task<Owner> GetOwnerByIdAsync(int ownerId, bool trackChanges);
        Task<Owner> GetOwnerWithDetailsAsync(int ownerId, bool trackChanges);

        Task<IEnumerable<Owner>> GetByIds(IEnumerable<int> ids, bool trackChanges);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }
}
