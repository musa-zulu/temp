using System.ComponentModel.DataAnnotations;

namespace AccountManagement.DB.Domain
{
    public class AccountStatus
    {
        [Key]
        public int Code { get; set; }

        public string Status { get; set; }

        public int AccountCode { get; set; }
        public virtual Account Account { get; set; }
    }
}