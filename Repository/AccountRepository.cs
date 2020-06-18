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

        public async Task<IEnumerable<Account>> GetAllAccountsAsync(bool trackChanges) => await
            FindAll(trackChanges).Include(ow=>ow.owner)
            .OrderBy(ac => ac.AccountType).ToListAsync();

        public async Task<Account> GetAccountWithDetailsAsync(int AccountId, bool trackChanges) =>
            await FindByCondition(ar => ar.Id.Equals(AccountId), trackChanges)
            .Include(ow=>ow.owner)
            .SingleOrDefaultAsync();

        public void CreateAccountForOwner(int ownerId, Account account)
        {
            account.OwnerId = ownerId;
            Create(account);
        }

        public async Task<Account> GetAccountAsync(int OwnerId, int accountId, bool trackChanges) =>
            await FindByCondition(ac=>ac.OwnerId.Equals(OwnerId) && ac.Id.Equals(accountId),trackChanges)
            .Include(ow=>ow.owner)
            .SingleOrDefaultAsync();

        public void DeleteAccount(Account account)
        {
            Delete(account);
        }
    }
}
