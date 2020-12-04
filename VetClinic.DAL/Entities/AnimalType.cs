using System.Collections.Generic;

namespace VetClinic.DAL.Entities
{
    class AnimalType : IBaseEntity
    {
        public int Id { get; set; }
        public string AnimalTypeName { get; set; }

        public ICollection<Animal> Animals { get; set; }
    }
}
