namespace SchoolAssistant.DAL.Interfaces
{
    public interface IPerson
    {
        public string FirstName { get; set; }
        public string? SecondName { get; set; }
        public string LastName { get; set; }
    }
}
