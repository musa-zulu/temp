using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.DB.Domain
{
    public class Person
    {
        public Person()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        public int Code { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}