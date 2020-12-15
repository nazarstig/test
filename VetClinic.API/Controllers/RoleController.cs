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
        public IActionResult Index()
        {
            var roles = RoleManager.Roles;
            var json = JsonSerializer.Serialize(roles);
            return Ok(json);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Show(string id)
        {
            var roles = await RoleManager.FindByIdAsync(id);
            var json = JsonSerializer.Serialize(roles);
            return Ok(json);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]RoleDto dto)
        {
            var RoleConfig = new MapperConfiguration(cfg => cfg.AddProfile<RoleProfile>());
            var roleMapper = new Mapper(RoleConfig);

            IdentityRole role = roleMapper.Map<RoleDto,IdentityRole>(dto);

            var result = await RoleManager.CreateAsync(role);
            
            return Created("/roles/create",result);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id, [FromBody] RoleDto dto)
        {
            var RoleConfig = new MapperConfiguration(cfg => cfg.AddProfile<RoleProfile>());
            var roleMapper = new Mapper(RoleConfig);

            IdentityRole inputRole = roleMapper.Map<RoleDto, IdentityRole>(dto);
            
            //todo:validation

            var role = await RoleManager.FindByIdAsync(id);

            role.Name = inputRole.Name;
            role.NormalizedName = inputRole.NormalizedName;

            _ = await RoleManager.UpdateAsync(role);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Destroy(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            var result = await RoleManager.DeleteAsync(role);

            return Ok(new { result = result.ToString()});
        }
    }
}
