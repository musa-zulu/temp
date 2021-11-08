using AccountManagement.DB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Contract.Implementation
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccountsAsync(PaginationFilter paginationFilter = null);
        Task<bool> CreateAccountAsync(Account account);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<bool> UpdateAccountAsync(Account accountToUpdate);
        Task<bool> DeleteAccountAsync(int accountId);
    }
}
