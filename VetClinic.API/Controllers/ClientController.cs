using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.DAL.Entities;
using VetClinic.API.DTO.ClientDto;
using VetClinic.BLL.Services.Interfaces;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
     
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;           
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateClientDto dto)
        {
            User user =  _mapper.Map<CreateClientDto, User>(dto);
            Client client = new Client();
            client = await _clientService.AddClient(user, client);
            ReadClientDto readDto = _mapper.Map<ReadClientDto>(client);
            return Created("/client/post", readDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, UpdateClientDto dto)
        {
            Client client = await _clientService.GetClient(id);
            if (client == null) return NotFound();
            else
            {
                User user = _mapper.Map<User>(dto);
                var res = await _clientService.PutClient(user, client);
                if (res)
                {
                    return NoContent();
                }
                else return NotFound();
            }
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Client>>> GetAsync()
        {
            var result = await _clientService.GetAllClients();
            ICollection<ReadClientDto> readClients = new List<ReadClientDto>();
            ReadClientDto dto;
            foreach(Client client in result)
            {
                dto = _mapper.Map<ReadClientDto>(client);
                readClients.Add(dto);
            }
            return Ok(readClients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetAsync(int id)
        {
            var res = await _clientService.GetClient(id);
            if (res == null) 
                return NotFound();
            else
            {
                ReadClientDto readClient = _mapper.Map<ReadClientDto>(res);
                return Ok(readClient);
            }               
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (await _clientService.DeleteClient(id))
                return NoContent();
            else return NotFound();
        }
    }
}
