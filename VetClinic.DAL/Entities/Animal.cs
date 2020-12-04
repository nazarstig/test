namespace VetClinic.DAL.Entities
{
    public class Animal : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public string Vasya { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int AnimalTypeId { get; set; }
        public AnimalType AnimalType { get; set; }
    }
}
