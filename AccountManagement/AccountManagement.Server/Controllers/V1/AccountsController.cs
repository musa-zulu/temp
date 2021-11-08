using AccountManagement.DB.Dtos;
using AccountManagement.Persistence.Features.AccountFeature.Commands;
using AccountManagement.Persistence.Features.AccountFeature.Queries;
using AccountManagement.Persistence.Features.PersonFeature.Commands;
using AccountManagement.Persistence.Features.PersonFeature.Queries;
using AccountManagement.Persistence.V1;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AccountManagement.Server.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountsController : ControllerBase
    {
        private IMediator _mediator;
        public IMediator Mediator
        {
            get { return _mediator ??= HttpContext.RequestServices.GetService<IMediator>(); }
            set
            {
                if (_mediator != null) throw new InvalidOperationException("Mediator is already set");
                _mediator = value;
            }
        }

        [HttpGet(ApiRoutes.Accounts.Get)]
        public async Task<IActionResult> Get([FromRoute] int accountId)
        {
            return Ok(await Mediator.Send(new GetAccountByIdQuery { AccountId = accountId }));
        }

        [HttpPost(ApiRoutes.Accounts.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrEditAccountDto accountDto)
        {
            CreateAccountCommand command = new()
            {
                AccountDto = accountDto
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut(ApiRoutes.Accounts.Update)]
        public async Task<IActionResult> Update([FromBody] CreateOrEditAccountDto accountDto)
        {
            UpdateAccountCommand command = new()
            {
                AccountDto = accountDto
            };
            return command.AccountDto.Code == default(int) ? BadRequest() : Ok(await Mediator.Send(command));
        }

        [HttpDelete(ApiRoutes.Accounts.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int accountId)
        {
            return Ok(await Mediator.Send(new DeleteAccountByIdCommand { AccountId = accountId }));
        }
    }
}
