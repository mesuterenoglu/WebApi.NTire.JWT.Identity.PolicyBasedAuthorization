using BLL.IServices;
using Common;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Models.CompanyUser;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/companyusers")]
    [ApiController]
    public class CompanyUserController : ControllerBase
    {
        private readonly ICompanyUserService _companyUserService;
        private readonly IUserService _userService;

        public CompanyUserController(ICompanyUserService companyUserService, IUserService userService)
        {
            _companyUserService = companyUserService;
            _userService = userService;
        }

        [Authorize("Admin")]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var companyUsers = await _companyUserService.GetAllAsync();
                return Ok(companyUsers);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet("getallactive")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var companyUsers = await _companyUserService.GetAllActiveAsync();
                return Ok(companyUsers);
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
                var companyUsers = await _companyUserService.GetAllInActiveAsync();
                return Ok(companyUsers);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var companyUser = await _companyUserService.GetbyIdAsync(id);
                if (companyUser != null)
                {
                    return Ok(companyUser);
                }
                return BadRequest(Messages.MissingCompanyUserById);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _companyUserService.AnyAsync(x => x.Id == id);
                if (result)
                {
                    await _companyUserService.DeleteAsync(id);
                    return Ok(Messages.Deleted);
                }
                return BadRequest(Messages.MissingCompanyUserById);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        [HttpDelete("removefromdb/{id}")]
        public async Task<IActionResult> RemoveFromDb(Guid id)
        {
            try
            {
                var result = await _companyUserService.AnyAsync(x => x.Id == id);
                if (result)
                {
                    await _companyUserService.RemoveFromDbAsync(id);
                    return Ok(Messages.Deleted);
                }
                return BadRequest(Messages.MissingCompanyUserById);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddCompanyUserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CompanyUserDto newCompanyUser = new CompanyUserDto();

                    var resultAny = await _userService.AnybyEmailAsync(model.Email);
                    if (resultAny)
                    {
                        var resultUser = await _companyUserService.AnyAsync(x => x.AppUser.Email == model.Email);
                        if (resultUser)
                        {
                            return BadRequest(Messages.DuplicateCompanyUser);
                        }
                        var appUser = await _userService.GetbyEmailAsync(model.Email);
                        newCompanyUser.AppUserId = appUser.Id;
                    }
                    else
                    {
                        AppUserDto appUser = new AppUserDto();
                        appUser.Id = Guid.NewGuid().ToString();
                        appUser.Email = model.Email;
                        await _userService.RegisterAsync(appUser, model.Password);

                        newCompanyUser.AppUserId = appUser.Id;
                    }

                    newCompanyUser.Id = Guid.NewGuid();
                    newCompanyUser.FirstName = model.FirstName;
                    newCompanyUser.LastName = model.LastName;

                    await _companyUserService.AddAsync(newCompanyUser);
                    return Ok(Messages.Completed);
                }
                return BadRequest(model);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCompanyUserModel model)
        {
            try
            {
                var companyUser = await _companyUserService.GetbyIdAsync(id);
                if (companyUser == null)
                {
                    return BadRequest(Messages.MissingCompanyUserById);
                }
                if (ModelState.IsValid)
                {
                    companyUser.FirstName = model.FirstName;
                    companyUser.LastName = model.LastName;
                    await _companyUserService.UpdateAsync(companyUser);

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
