

using Common.Token;
using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IUserService
    {
        Task<string> RegisterAsync(AppUserDto entity, string password);
        Task<Token> CreateTokenAsync(string userName, string password);
        Task<bool> CheckPasswordAsync(string userName, string password);
        Task<string> NewPasswordAsync(string email, string password, string newPassword);
        Task<string> NewEmailAsync(string email, string password, string newEmail);
        Task<AppUserDto> GetbyIdAsync(string id);
        Task<AppUserDto> GetbyEmailAsync(string email);
        Task<bool> AnybyEmailAsync(string email);
        Task<bool> AnybyIdAsync(string id);
        List<AppUserDto> GetAllActive();
    }
}
