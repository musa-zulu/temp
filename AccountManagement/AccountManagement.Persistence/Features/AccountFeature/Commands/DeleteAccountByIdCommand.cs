using AccountManagement.Persistence.Contract.Implementation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.AccountFeature.Commands
{
    public class DeleteAccountByIdCommand : IRequest<bool>
    {
        public int AccountId { get; set; }
        public class DeleteAccountByIdCommandHandler : IRequestHandler<DeleteAccountByIdCommand, bool>
        {
            private readonly IAccountService _accountService;
            public DeleteAccountByIdCommandHandler(IAccountService accountService)
            {
                _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            }
            public async Task<bool> Handle(DeleteAccountByIdCommand request, CancellationToken cancellationToken)
            {
                var results = await _accountService.DeleteAccountAsync(request.AccountId);
                return results;
            }
        }
    }
}