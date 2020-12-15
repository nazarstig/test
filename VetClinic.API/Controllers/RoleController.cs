using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using VetClinic.API.DTO;
using VetClinic.API.Mapping;

namespace VetClinic.API.Controllers
{
    [Route("roles")]
    public class RoleController : Controller
    {
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }

        public RoleManager<IdentityRole> RoleManager { get; }

        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
            var roles = RoleManager.Roles;
            var json = JsonSerializer.Serialize(roles);
            return Ok(json);
        }

        [Route("/{id}")]
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var roles = await RoleManager.FindByIdAsync(id);
            var json = JsonSerializer.Serialize(roles);
            return Ok(json);
        }

        [Route("/create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RoleDto dto)
        {
            var RoleConfig = new MapperConfiguration(cfg => cfg.AddProfile<RoleProfile>());
            var roleMapper = new Mapper(RoleConfig);

            IdentityRole role = roleMapper.Map<RoleDto,IdentityRole>(dto);

            var roles = await RoleManager.CreateAsync(role);
            var json = JsonSerializer.Serialize(roles);
            return Ok(json);
        }
    }
}
