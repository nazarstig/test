using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.API.DTO.ProcedureDTO;
using AutoMapper;
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
        public async Task<IActionResult> PostAsync(CreateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<CreateProcedureDTO, Procedure>(procedureDTO);
            await _procedureService.AddProcedure(procedure);
            return CreatedAtAction(nameof(GetAsync), new { id = procedure.Id }, procedure);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateProcedureDTO procedureDTO)
        {
            Procedure procedure = _mapper.Map<UpdateProcedureDTO, Procedure>(procedureDTO);
            if (await _procedureService.PutProcedure(id, procedure))
                return NoContent();
            else return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _procedureService.DeleteProcedure(id))
                return NoContent();
            else return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ReadProcedureDTO>>> GetAsync()
        {            
            var res =  await _procedureService.GetAllProcedures();
            ICollection<ReadProcedureDTO> readProcedures = new List<ReadProcedureDTO>();
            ReadProcedureDTO dto;
            foreach(Procedure procedure in res)
            {
                dto = _mapper.Map<ReadProcedureDTO>(procedure);
                readProcedures.Add(dto);
            }
            return Ok(readProcedures);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Procedure>> GetAsync(int id)
        {
            var result = await _procedureService.GetProcedure(id);
            if (result != null)
            {
                ReadProcedureDTO readDto = _mapper.Map<ReadProcedureDTO>(result);
                return Ok(readDto);
            }
            else return NotFound();
        }

    }
}
