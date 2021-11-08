using AccountManagement.DB.Domain;
using AccountManagement.DB.Dtos;
using AccountManagement.Persistence.Contract.Implementation;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.PersonFeature.Commands
{
    public class CreatePersonCommand : IRequest<bool>
    {
        public CreateOrUpdatePersonDto PersonDto { get; set; }

        public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, bool>
        {
            private readonly IPersonService _personService;
            private readonly IMapper _mapper;
            public CreatePersonCommandHandler(IPersonService personService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _personService = personService ?? throw new ArgumentNullException(nameof(personService));
            }
            public async Task<bool> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
            {
                var person = _mapper.Map<Person>(request.PersonDto);                            
                var personSaved = false;
                if (person != null)
                {
                    personSaved = await _personService.CreatePersonAsync(person);
                }
                return personSaved;
            }
        }
    }
}