using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
        public void CreateAccount(Account account)
        {
           
            Create(account);
        }

        public async Task<IEnumerable<Account>> GetAllOwnersAsync(bool trackChanges) => await
            FindAll(trackChanges).Include(ow=>ow.owner).OrderBy(ac => ac.AccountType).ToListAsync();


        public async Task<Account> GetOwnerWithDetailsAsync(int ArticuloId, bool trackChanges) =>

            await FindCondition(ar => ar.Id.Equals(ArticuloId), trackChanges).FirstOrDefaultAsync();

   
    }
}
