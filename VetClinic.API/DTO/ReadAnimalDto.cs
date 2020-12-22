namespace VetClinic.API.DTO
{
    public class ReadAnimalDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public ReadAnimalTypeDto AnimalTypeName { get; set; }
    }
}
