using AccountManagement.DB.Domain;
using AccountManagement.DB.Dtos;
using AccountManagement.Persistence.Contract.Implementation;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.AccountFeature.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public CreateOrEditAccountDto AccountDto { get; set; }

        public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
        {
            private readonly IAccountService _accountService;
            private readonly IMapper _mapper;
            public CreateAccountCommandHandler(IAccountService accountService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            }
            public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
            {
                var account = _mapper.Map<Account>(request.AccountDto);
                var accountSaved = false;
                if (account != null)
                {
                    accountSaved = await _accountService.CreateAccountAsync(account);
                }
                return accountSaved;
            }
        }
    }
}