using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.BLL.Services;
using VetClinic.BLL.Services.Realizations;
using VetClinic.DAL.Entities;
using VetClinic.API.Mapping;
using VetClinic.API.DTO.ClientDTO;
using Microsoft.AspNetCore.Identity;
using VetClinic.BLL.Services.Interfaces;
using VetClinic.API.DTO.ClientDto;

namespace VetClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        private IMapper _mapper;
     
        public ClientController(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;           
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            User user =  _mapper.Map<CreateClientDto, User>(dto);
            Client client = new Client();
            client = await _clientService.AddClient(user, client);
            return CreatedAtAction(nameof(Show), new { Id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateClientDto dto)
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
        public async Task<ActionResult<ICollection<Client>>> Index()
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
        public async Task<ActionResult<Client>> Show(int id)
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
    }
}
