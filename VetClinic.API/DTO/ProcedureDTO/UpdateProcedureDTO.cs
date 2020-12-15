using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.API.DTO.ProcedureDTO
{
    public class UpdateProcedureDTO
    {
        public int Id { get; set; }
        public string ProcedureName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSelectable { get; set;}
    }
}
