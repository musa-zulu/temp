using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Features.PersonFeature.Queries
{
    public class GetAllPeopleQuery : IRequest<IEnumerable<Person>>
    {
        public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, IEnumerable<Person>>
        {
            private readonly IPersonService _personService;
            public GetAllPeopleQueryHandler(IPersonService personService)
            {
                _personService = personService ?? throw new ArgumentNullException(nameof(personService));
            }
            public async Task<IEnumerable<Person>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
            {
                var people = await _personService.GetPeopleAsync();
                if (people == null)
                {
                    return null;
                }
                return people.AsReadOnly();
            }
        }
    }
}
