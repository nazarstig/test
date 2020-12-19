using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VetClinic.API.DTO.Role;

namespace VetClinic.API.Controllers
{
    [Route("roles")]
    public class RoleController : Controller
    {
        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            RoleManager = roleManager;
            Mapper = mapper;
        }

        public RoleManager<IdentityRole> RoleManager { get; }
        public IMapper Mapper { get; }

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            var roles = RoleManager.Roles;
            IEnumerable<RoleDto> roleDtos = Mapper.Map< IEnumerable<IdentityRole>, IEnumerable <RoleDto> >(roles);

            return Ok(roleDtos);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Show(string id)
        {
            IdentityRole role = await RoleManager.FindByIdAsync(id);
            if(role == null)
            {
                return NotFound();
            }
            RoleDto roleDto = Mapper.Map<IdentityRole, RoleDto>(role);
            return Ok(roleDto);
        }

        [Route("")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto dto)
        {
            IdentityRole role = Mapper.Map<CreateRoleDto, IdentityRole>(dto);

            _ = await RoleManager.CreateAsync(role);
            
            return Created("/roles/", dto);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update(string id, CreateRoleDto dto)
        {
            IdentityRole inputRole = Mapper.Map<CreateRoleDto, IdentityRole>(dto);

            var role = await RoleManager.FindByIdAsync(id);

            role.Name = inputRole.Name;
            role.NormalizedName = inputRole.Name.ToUpper();

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
