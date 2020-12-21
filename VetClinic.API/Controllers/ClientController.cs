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
    public class ClientController : ControllerBase
    {
        private IClientService _clientService;
        private IUserService _userService;
        private IMapper _mapper;

        public ClientController(IClientService clientService, IUserService userService, IMapper mapper)
        {
            _clientService = clientService;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Create(CreateClientDto dto)
        {
            User user =  _mapper.Map<CreateClientDto, User>(dto);
            IdentityRole role = new IdentityRole { Name = "member" }; 
            var (res, id) = await _userService.CreateUser(user, new List<IdentityRole> { role });
            string userId;
            Client client = new Client { };
            if (res)
            {
                userId = id;
                client = new Client { UserId = userId };
                await _clientService.AddClient(client);
            }
            return CreatedAtAction(nameof(Show), new { Id = client.Id }, client);
        }

        public async Task<IActionResult> Update(int id, UpdateClientDto dto)
        {
            var getClient = await Show(id);
            if (getClient == null) return NotFound();
            else
            {
                User user = _mapper.Map<User>(dto);
                IdentityRole role = new IdentityRole { Name = "member" };
                var resUser = await _userService.UpdateUser(user, new List<IdentityRole> { role });
                if (resUser)
                {
                    return NoContent();
                }
                else return NotFound();
            }

        }
        public async Task<ActionResult<ICollection<Client>>> Index()
        {
            //UpdateClientDto createDto = new UpdateClientDto
            //{
            //    UserName = "stepKyrych",
            //    FirstName = "Stepan",
            //    LastName = "Kyrychenko",
            //    Email = "kyrych32@gmail.com",
            //    PhoneNumber = "4444445444",
            //    Password = "Rolulu@15"
            //};
            //await Update(6, createDto);
            //var client6 = await Show(6);
            var result = await _clientService.GetAllClients();
            return Ok(result);
        }

        public async Task<ActionResult<Client>> Show(int id)
        {
            var res = await _clientService.GetClient(id);
            if (res == null) 
                return NotFound();
            else return Ok(res);
        }
    }
}
