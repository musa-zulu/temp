using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;

namespace AccountManagement.Persistence.Features.PersonFeature.Queries
{
    public class GetPeopleByIdQuery : IRequest<Person>
    {
        public int PersonId { get; set; }
        public class GetUserByIdQueryHandler : IRequestHandler<GetPeopleByIdQuery, Person>
        {
            private readonly IPersonService _userService;
            public GetUserByIdQueryHandler(IPersonService userService)
            {
                _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            }
            public async Task<Person> Handle(GetPeopleByIdQuery request, CancellationToken cancellationToken)
            {
                var people = _userService.GetPersonByIdAsync(request.PersonId);
                if (people == null) return null;
                return await people;
            }
        }
    }
}
