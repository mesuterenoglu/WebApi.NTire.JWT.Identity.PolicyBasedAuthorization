using BLL.IServices;
using Common;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Company;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ICompanyUserService _companyUserService;
        private readonly IRoleService _roleService;

        public CompanyController(ICompanyService companyService,ICompanyUserService companyUserService,IRoleService roleService)
        {
            _companyService = companyService;
            _companyUserService = companyUserService;
            _roleService = roleService;
        }

        [Authorize("Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var companies = await _companyService.GetAllAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("getallactive")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var companies = await _companyService.GetAllActiveAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [Authorize("Admin")]
        [HttpGet("getallinactive")]
        public async Task<IActionResult> GetAllInActive()
        {
            try
            {
                var companies = await _companyService.GetAllInActiveAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [Authorize(Policy = "SameCompany")]
        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var company = await _companyService.GetbyIdAsync(id);
                if (company != null)
                {
                    return Ok(company);
                }
                return BadRequest(Messages.MissingCompanyById);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "SameCompany")]
        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _companyService.AnyAsync(x => x.Id == id);
                if (result)
                {
                    await _companyService.DeleteAsync(id);
                    return Ok(Messages.Deleted);
                }
                return BadRequest(Messages.MissingCompanyById);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [Authorize(Policy = "SameCompany")]
        [HttpDelete("removefromdb/{id}")]
        public async Task<IActionResult> RemoveFromDb(Guid id)
        {
            try
            {
                var result = await _companyService.AnyAsync(x => x.Id == id);
                if (result)
                {
                    await _companyService.RemoveFromDbAsync(id);
                    return Ok(Messages.Deleted);
                }
                return BadRequest(Messages.MissingCompanyById);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddCompanyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyDto newCompany = new CompanyDto();
                    newCompany.Id = Guid.NewGuid();
                    newCompany.CompanyName = model.CompanyName;
                    newCompany.PhoneNumber = model.PhoneNumber;
                    newCompany.Address = model.Address;

                    var userId = HttpContext.User.Claims.Where(c => c.Type == "UserId").First().Value;

                    var companyUser = await _companyUserService.GetCompanyUserbyAppUserIdAsync(userId);

                    if (companyUser == null)
                    {
                        return BadRequest(Messages.MissingCompanyUserCanCreate);
                    }

                    await _companyService.AddAsync(newCompany);

                    companyUser.CompanyId = newCompany.Id;

                    await _companyUserService.UpdateAsync(companyUser);

                    var resultRole = await _roleService.AssignRoleByUserIdAsync(userId, "CompanyOwner");

                    if (resultRole == Messages.Completed)
                    {
                        return Ok(Messages.Completed);
                    }

                    return BadRequest(Messages.CompanyCreatedButRoleDidntAssign);
                }
                
                return BadRequest(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [Authorize(Policy = "SameCompany")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyModel model)
        {
            try
            {
                var company = await _companyService.GetbyIdAsync(id);
                if (company == null)
                {
                    return BadRequest(Messages.MissingCompanyById);
                }
                if (ModelState.IsValid)
                {
                    company.CompanyName = model.CompanyName;
                    company.Address = model.Address;
                    company.PhoneNumber = model.PhoneNumber;

                    await _companyService.UpdateAsync(company);

                    return Ok(Messages.Completed);
                }
                return BadRequest(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }


    }
}
