using BLL.IServices;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.Role;

namespace WebApi.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRoles([FromBody] CreateRoleModel createRoleModel)
        {
            var result = await _roleService.RoleExistAsync(createRoleModel.RoleName);
            if (!result)
            {
                await _roleService.CreateRoleAsync(createRoleModel.RoleName);

                return Ok(Messages.Completed);

            }
            return BadRequest(Messages.DuplicateRole);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getallactive")]
        public IActionResult GetActiveRoles()
        {
            var roles = _roleService.GetActiveRoles();
            if (roles != null )
            {
                return Ok(roles);
            }
            return BadRequest(Messages.MissingUserRole);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteRole([FromBody]DeleteRoleModel model)
        {
            var resultRole = await _roleService.RoleExistAsync(model.RoleName);
            if (resultRole)
            {
                await _roleService.DeleteRoleAsync(model.Id);
                return Ok(Messages.Deleted);
            }
            return BadRequest(Messages.MissingUserRole);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("removefromdb")]
        public async Task<IActionResult> RemoveRoleFromDb([FromBody] DeleteRoleModel model)
        {
            var resultRole = await _roleService.RoleExistAsync(model.RoleName);
            if (resultRole)
            {
                await _roleService.RemoveRoleFromDbAsync(model.Id);
                return Ok(Messages.Deleted);
            }
            return BadRequest(Messages.MissingUserRole);
        }

        [Authorize(Roles = "Admin, CompanyOwner")]
        [HttpPost]
        public async Task<IActionResult> AssignnRoleById([FromBody] AssginRoleModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.AssignRoleAsync(model.UserEmail, model.RoleName);
                if (result == Messages.Completed)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(model);
        }

    }
}
