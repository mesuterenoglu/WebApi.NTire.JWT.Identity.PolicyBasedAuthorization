using AutoMapper;
using BLL.IServices;
using Common;
using Common.Token;
using Core.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration)   
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<bool> AnybyEmailAsync(string email)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnybyIdAsync(string id)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id);
                if (appUser == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public List<AppUserDto> GetAllActive()
        {
            try
            {
                var users = _userManager.Users.Where(x => x.IsActive == true).ToList();
                var list = _mapper.Map<List<AppUserDto>>(users);
                return list;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AppUserDto> GetbyEmailAsync(string email)
        {
            try
            {
                var appUser = await _userManager.FindByEmailAsync(email);
                if (appUser == null)
                {
                    throw new InvalidOperationException("Böyle bir kullanıcı bulunamadı!");
                }
                var appUserDto = _mapper.Map<AppUserDto>(appUser);

                return appUserDto;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<AppUserDto> GetbyIdAsync(string id)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id);
                if (appUser == null)
                {
                    throw new InvalidOperationException("Böyle bir kullanıcı bulunamadı!");
                }
                var appUserDto = _mapper.Map<AppUserDto>(appUser);

                return appUserDto;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<Token> CreateTokenAsync(string userName, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    TokenGenerator tokenGenerator = new TokenGenerator(_configuration, _userManager);
                    Token token = await tokenGenerator.CreateToken(user);

                    return token;
                }
                throw new InvalidOperationException(Messages.MissingUserEmail);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> NewPasswordAsync(string email, string password, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);

                if (user!=null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, password, newPassword);

                    if (result.Succeeded)
                    {
                        return Messages.Completed;
                    }

                    return Messages.Failed;
                }

                return Messages.MissingUserEmail;

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> RegisterAsync(AppUserDto entity, string password)
        {
            try
            {
                var resultEmail = await AnybyEmailAsync(entity.Email);
                if (resultEmail)
                {
                    return Messages.DuplicateUserEmail;
                }

                var appUser = _mapper.Map<AppUser>(entity);

                appUser.IsActive = true;
                appUser.UserName = entity.Email;
                appUser.CreatedDate = DateTime.Now;

                var result = await _userManager.CreateAsync(appUser, password);

                if (result.Succeeded)
                {
                    return Messages.Completed;
                }
                return Messages.Failed;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> CheckPasswordAsync(string userName, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    throw new InvalidOperationException(Messages.MissingUserEmail);
                }
                var result = await _userManager.CheckPasswordAsync(user, password);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<string> NewEmailAsync(string email, string password, string newEmail)
        {
            try
            {
                var resultCheck = await CheckPasswordAsync(email, password);
                if (resultCheck)
                {
                    var user = await _userManager.FindByEmailAsync(email);
                    user.Email = newEmail;
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Messages.Completed;
                    }
                    return Messages.Failed;
                }
                return Messages.InvalidPassword;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
