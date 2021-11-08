using System.Collections.Generic;

namespace AccountManagement.DB.Dtos
{
    public class AccountDto
    {
        public AccountDto()
        {
            AccountStatus = new HashSet<AccountStatusDto>();
            Transactions = new HashSet<TransactionDto>();
        }
        public int Code { get; set; }
        public int PersonCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal OutstandingBalance { get; set; }

        public virtual PersonDto Person { get; set; }
        public virtual ICollection<AccountStatusDto> AccountStatus { get; set; }
        public virtual ICollection<TransactionDto> Transactions { get; set; }
    }
}
