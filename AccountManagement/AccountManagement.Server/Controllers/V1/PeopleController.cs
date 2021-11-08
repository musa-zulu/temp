using AccountManagement.DB.Dtos;
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
    public class PeopleController : ControllerBase
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

        [HttpGet(ApiRoutes.People.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPeopleQuery()));
        }

        [HttpGet(ApiRoutes.People.Get)]
        public async Task<IActionResult> Get([FromRoute] int personId)
        {
            return Ok(await Mediator.Send(new GetPeopleByIdQuery { PersonId = personId }));
        }

        [HttpPost(ApiRoutes.People.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdatePersonDto personDto)
        {
            CreatePersonCommand command = new()
            {
                PersonDto = personDto
            };
            return Ok(await Mediator.Send(command));
        }

        [HttpPut(ApiRoutes.People.Update)]
        public async Task<IActionResult> Update([FromBody] CreateOrUpdatePersonDto personDto)
        {
            UpdatePersonCommand command = new()
            {
                PersonDto = personDto
            };
            return command.PersonDto.Code == default(int) ? BadRequest() : Ok(await Mediator.Send(command));
        }

        [HttpDelete(ApiRoutes.People.Delete)]
        public async Task<IActionResult> Delete([FromRoute]  int personId)
        {
            return Ok(await Mediator.Send(new DeletePersonByIdCommand { PersonId = personId }));
        }

    }
}
