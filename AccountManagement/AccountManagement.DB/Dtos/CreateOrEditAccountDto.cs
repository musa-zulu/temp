namespace AccountManagement.DB.Dtos
{
    public class CreateOrEditAccountDto
    {
        public int Code { get; set; }

        public int PersonCode { get; set; }
        public string AccountNumber { get; set; }
        public decimal OutstandingBalance { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}
