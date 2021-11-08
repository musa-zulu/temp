using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.AccountFeature.Queries
{
    public class GetAccountByIdQuery : IRequest<Account>
    {
        public int AccountId { get; set; }
        public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, Account>
        {
            private readonly IAccountService _accountService;
            public GetAccountByIdQueryHandler(IAccountService accountService)
            {
                _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            }
            public async Task<Account> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
            {
                var account = _accountService.GetAccountByIdAsync(request.AccountId);
                if (account == null) return null;
                return await account;
            }
        }
    }
}
