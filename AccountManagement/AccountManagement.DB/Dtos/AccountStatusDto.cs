namespace AccountManagement.DB.Dtos
{
    public class AccountStatusDto
    {
        public int Code { get; set; }

        public string Status { get; set; }
        public int AccountCode { get; set; }
        public virtual AccountDto Account { get; set; }
    }
}
