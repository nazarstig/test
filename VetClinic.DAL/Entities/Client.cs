namespace VetClinic.DAL.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}