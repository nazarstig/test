namespace VetClinic.DAL.Entities
{
    public class Client : IBaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}