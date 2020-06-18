using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IAccountRepository : IRepositoryBase<Account>
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync(bool trackChanges);
        Task<Account> GetAccountWithDetailsAsync(int accountId, bool trackChanges);
        Task<Account> GetAccountAsync(int OwnerId,int accountId, bool trackChanges);
        void CreateAccountForOwner(int ownerId, Account account);
        void DeleteAccount(Account account);

    }
}
