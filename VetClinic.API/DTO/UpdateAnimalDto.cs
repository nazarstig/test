﻿namespace VetClinic.API.DTO
{
    public class UpdateAnimalDto
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Photo { get; set; }
        public int AnimalTypeId { get; set; }
    }
}
