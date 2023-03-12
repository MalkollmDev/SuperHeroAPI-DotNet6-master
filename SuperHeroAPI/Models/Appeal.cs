namespace SuperHeroAPI.Models
{
    public class Appeal
    {
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
    }
}