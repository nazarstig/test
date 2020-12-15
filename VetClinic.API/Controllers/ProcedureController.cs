using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.BLL.Services;
using VetClinic.API.DTO.ProcedureDTO;
using AutoMapper;

namespace VetClinic.API.Controllers
{
    public class ProcedureController : Controller
    {
        private ProcedureService _procedureService;

        private IMapper _mapper;
        public ProcedureController(ProcedureService procedureService, IMapper mapper)
        {
            _procedureService = procedureService;
            _mapper = mapper;
        }
        public void AddProcedure(CreateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<CreateProcedureDTO, Procedure>(procedureDTO);
            _procedureService.AddProcedure(procedure);            
        }

        [HttpPut]
        public async Task<IActionResult> PutProcedure(UpdateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<UpdateProcedureDTO, Procedure>(procedureDTO);
            if (await _procedureService.PutProcedure(procedure))
                return Ok();
            else return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProcedure(DeleteProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<DeleteProcedureDTO, Procedure>(procedureDTO);
            if (await _procedureService.DeleteProcedure(procedure))
                return Ok();
            else return NotFound();
        }

        [HttpGet]
        public ActionResult<ICollection<string>> GetAllProceduresNames()
        {
            var names = _procedureService.GetAllProceduresNames();
            if (names == null) return NotFound();
            else return Ok(names);
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Procedure>>> GetAllProcedures()
        {            
            var res =  await _procedureService.GetAllProcedures();
            if (res == null) return NotFound();
            else return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<Procedure>> GetProcedure(ReadProcedureDTO dto)
        {
            //CreateProcedureDTO createTemp = new CreateProcedureDTO { ProcedureName = "cleaning", Price = 100M, Description = "for all animals", IsSelectable = true};
            //AddProcedure(createTemp);
            //UpdateProcedureDTO updateTemp = new UpdateProcedureDTO { Id = 9, Description = "for cats only", Price = 100M, IsSelectable = false };
            //var res = await PutProcedure(updateTemp);
            //DeleteProcedureDTO deleteTemp = new DeleteProcedureDTO { Id = 10 };
            DeleteProcedureDTO deleteTemp = new DeleteProcedureDTO { Id = 3};
            int id = deleteTemp.Id;//_mapper.Map<ReadProcedureDTO, int>(dto);
            var result = await _procedureService.GetProcedure(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
