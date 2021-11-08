using AccountManagement.Persistence.Contract.Implementation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.PersonFeature.Commands
{
    public class DeletePersonByIdCommand : IRequest<bool>
    {
        public int PersonId { get; set; }
        public class DeletePersonByIdCommandHandler : IRequestHandler<DeletePersonByIdCommand, bool>
        {
            private readonly IPersonService _personService;
            public DeletePersonByIdCommandHandler(IPersonService personService)
            {
                _personService = personService ?? throw new ArgumentNullException(nameof(personService));
            }
            public async Task<bool> Handle(DeletePersonByIdCommand request, CancellationToken cancellationToken)
            {
                var results = await _personService.DeletePersonAsync(request.PersonId);
                return results;
            }
        }
    }
}