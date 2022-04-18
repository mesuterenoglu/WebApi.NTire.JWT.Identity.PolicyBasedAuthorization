using BLL.IServices;
using Common;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Models.User;

namespace WebApi.Controllers
{
    [Route("api/appusers")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var resultAny = await _userService.AnybyEmailAsync(model.Email);
            if (resultAny)
            {
                return BadRequest(Messages.DuplicateUserEmail);
            }
            AppUserDto appUser = new AppUserDto();
            appUser.Email = model.Email;
            appUser.Id = Guid.NewGuid().ToString();
            var resultRegister = await _userService.RegisterAsync(appUser, model.Password);
            if (resultRegister == Messages.Completed)
            {
                return StatusCode(201, Messages.Completed);
            }
            return BadRequest(resultRegister);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var resultAny = await _userService.AnybyEmailAsync(model.Email);
            if (!resultAny)
            {
                return BadRequest(Messages.MissingUserEmail);
            }
            var resultPassword = await _userService.CheckPasswordAsync(model.Email, model.Password);
            if (!resultPassword)
            {
                return BadRequest(Messages.InvalidPassword);
            }
            var token = await _userService.CreateTokenAsync(model.Email, model.Password);
            return Ok(token);
        }
        [HttpPost("newpassword")]
        public async Task<IActionResult> NewPassword([FromBody] NewPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var resultUser = await _userService.GetbyEmailAsync(model.Email);
                if (resultUser == null)
                {
                    return BadRequest(Messages.MissingUserEmail);
                }
                var result = await _userService.NewPasswordAsync(model.Email, model.CurrentPassword, model.NewPassword);
                if (result == Messages.Completed)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(model);
        }

        [HttpPost("newemail")]
        public async Task<IActionResult> NewEmail([FromBody] NewEmailModel model)
        {
            if (ModelState.IsValid)
            {
                var resultUser = await _userService.GetbyEmailAsync(model.Email);
                if (resultUser == null)
                {
                    return BadRequest(Messages.MissingUserEmail);
                }
                var result = await _userService.NewEmailAsync(model.Email, model.Password, model.NewEmail);
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
