using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.BLL.Services;
using VetClinic.API.DTO.ProcedureDTO;
using AutoMapper;
using System.Net;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedureController : ControllerBase
    {
        private IProcedureService _procedureService;

        private IMapper _mapper;

       
        public ProcedureController(IProcedureService procedureService, IMapper mapper)
        {
            _procedureService = procedureService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<CreateProcedureDTO, Procedure>(procedureDTO);
            await _procedureService.AddProcedure(procedure);
            return CreatedAtAction(nameof(Show), new { id = procedure.Id }, procedure);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<UpdateProcedureDTO, Procedure>(procedureDTO);
            if (await _procedureService.PutProcedure(procedure))
                return NoContent();
            else return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> Destroy(int id)
        {
            if (await _procedureService.DeleteProcedure(id))
                return NoContent();
            else return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<Procedure>>> Index()
        {            
            var res =  await _procedureService.GetAllProcedures();
            return Ok(res);
        }

        [HttpGet]
        public async Task<ActionResult<Procedure>> Show(int id)
        {
            //CreateProcedureDTO createTemp = new CreateProcedureDTO { ProcedureName = "cleaning", Price = 100M, Description = "for all animals", IsSelectable = true};
            //AddProcedure(createTemp);
            //UpdateProcedureDTO updateTemp = new UpdateProcedureDTO { Id = 20, Description = "for cats only", Price = 100M, IsSelectable = false };
            //var res = await PutProcedure(updateTemp);
            //DeleteProcedureDTO deleteTemp = new DeleteProcedureDTO { Id = 10 };
            
            //int id = deleteTemp.Id;//_mapper.Map<ReadProcedureDTO, int>(dto);
            var result = await _procedureService.GetProcedure(id);
            DeleteProcedureDTO deleteTemp = new DeleteProcedureDTO { Id = 7};
            // await DeleteProcedure(20);
            //if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
