using AccountManagement.DB.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Persistence.Contract.Implementation
{
    public interface IPersonService
    {
        Task<List<Person>> GetPeopleAsync(PaginationFilter paginationFilter = null);
        Task<bool> CreatePersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(int personId);
        Task<bool> UpdatePersonAsync(Person personToUpdate);
        Task<bool> DeletePersonAsync(int personId);
    }
}
