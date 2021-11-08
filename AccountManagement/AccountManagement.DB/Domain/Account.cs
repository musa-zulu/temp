using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.DB.Domain
{
    public class Account
    {
        public Account()
        {
            AccountStatus = new HashSet<AccountStatus>();
            Transactions = new HashSet<Transaction>();
        }

        [Key]
        public int Code { get; set; }

        public int PersonCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal OutstandingBalance { get; set; }

        public virtual Person PersonCodeNavigation { get; set; }
        public virtual ICollection<AccountStatus> AccountStatus { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}