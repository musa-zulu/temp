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
    public class UpdatePersonCommand : IRequest<bool>
    {
        public CreateOrUpdatePersonDto PersonDto { get; set; }

        public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, bool>
        {
            private readonly IPersonService _personService;
            private readonly IMapper _mapper;
            public UpdatePersonCommandHandler(IPersonService personService, IMapper mapper)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _personService = personService ?? throw new ArgumentNullException(nameof(personService));
            }
            public async Task<bool> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
            {
                var person = _mapper.Map<Person>(request.PersonDto);
                var isSaved = false;
                if (person != null)
                {
                    isSaved = await _personService.UpdatePersonAsync(person);
                }
                return isSaved;
            }
        }
    }
}