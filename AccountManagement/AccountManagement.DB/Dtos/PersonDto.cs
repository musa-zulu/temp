using System.Collections.Generic;

namespace AccountManagement.DB.Dtos
{
    public class PersonDto
    {
        public PersonDto()
        {
            Accounts = new HashSet<AccountDto>();
        }
        
        public int Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public virtual ICollection<AccountDto> Accounts { get; set; }
    }
}
