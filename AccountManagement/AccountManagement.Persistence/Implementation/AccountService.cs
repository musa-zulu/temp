using AccountManagement.DB;
using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IApplicationDbContext _dataContext;
        public AccountService(IApplicationDbContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            _dataContext.Accounts.Add(account);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Account>> GetAccountsAsync(PaginationFilter paginationFilter = null)
        {
            var queryable = _dataContext.Accounts.AsQueryable();

            if (paginationFilter == null)
            {
                return await queryable
                    .Include(x => x.AccountStatus)
                    .Include(x => x.PersonCodeNavigation)
                    .Include(X => X.Transactions)
                    .ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Include(x => x.AccountStatus)
                    .Include(x => x.PersonCodeNavigation)
                    .Include(X => X.Transactions)
                    .Skip(skip).Take(paginationFilter.PageSize)
                    .ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            return await _dataContext.Accounts
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Code == accountId);
        }

        public async Task<bool> UpdateAccountAsync(Account accountToUpdate)
        {
            _dataContext.Accounts.Update(accountToUpdate);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAccountAsync(int accountId)
        {
            var account = await GetAccountByIdAsync(accountId);

            if (account == null)
            {
                return false;
            }

            _dataContext.Accounts.Remove(account);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
