using System;

namespace AccountManagement.DB.Dtos
{
    public class TransactionDto
    {
        public int Code { get; set; }

        public int AccountCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public virtual AccountDto Account { get; set; }
    }
}
