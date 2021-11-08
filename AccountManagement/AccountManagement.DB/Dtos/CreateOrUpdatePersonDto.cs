namespace AccountManagement.DB.Dtos
{
    public class CreateOrUpdatePersonDto
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }        
    }
}
