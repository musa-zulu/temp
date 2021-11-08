using System;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.DB.Domain
{
    public class Transaction
    {
        [Key]
        public int Code { get; set; }

        public int AccountCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CaptureDate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public virtual Account Account { get; set; }
    }
}