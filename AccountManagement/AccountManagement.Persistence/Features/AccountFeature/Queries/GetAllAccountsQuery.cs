using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.AccountFeature.Queries
{
    public class GetAllAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, IEnumerable<Account>>
        {
            private readonly IAccountService _accountService;
            public GetAllAccountsQueryHandler(IAccountService accountService)
            {
                _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            }
            public async Task<IEnumerable<Account>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
            {
                var account = await _accountService.GetAccountsAsync();
                if (account == null)
                {
                    return null;
                }
                return account.AsReadOnly();
            }
        }
    }
}
