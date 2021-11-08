using AccountManagement.DB;
using AccountManagement.DB.Domain;
using AccountManagement.Persistence.Contract.Implementation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Implementation
{
    public class PersonService : IPersonService
    {
        private readonly IApplicationDbContext _dataContext;
        public PersonService(IApplicationDbContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public async Task<bool> CreatePersonAsync(Person person)
        {
            _dataContext.Persons.Add(person);
            return await _dataContext.SaveChangesAsync() > 0;
        }
        
        public async Task<List<Person>> GetPeopleAsync(PaginationFilter paginationFilter = null)
        {
            var queryable = _dataContext.Persons.AsQueryable();

            if (paginationFilter == null)
            {
                return await queryable.Include(x => x.Accounts).ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable.Include(x => x.Accounts).Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<Person> GetPersonByIdAsync(int personId)
        {
            return await _dataContext.Persons
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Code == personId);
        }

        public async Task<bool> UpdatePersonAsync(Person personToUpdate)
        {
            _dataContext.Persons.Update(personToUpdate);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            var person = await GetPersonByIdAsync(personId);

            if (person == null)
            {
                return false;
            }

            _dataContext.Persons.Remove(person);
            return await _dataContext.SaveChangesAsync() > 0;
        }
    }
}
