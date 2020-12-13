using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.DTO.PositionDTO
{
    public class UpdatePositionDTO
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public decimal Salary { get; set; }
    }
}
