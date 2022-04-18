

using Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface IRoleService
    {
        Task CreateRoleAsync(string role);
        Task<string> AssignRoleAsync(string email, string role);
        Task<string> AssignRoleByUserIdAsync(string id, string role);
        Task<bool> RoleExistAsync(string role);
        Task<bool> IsInRoleAsync(string email, string role);
        List<AppRoleDto> GetActiveRoles();
        Task<string> DeleteRoleAsync(string id);
        Task<string> RemoveRoleFromDbAsync(string id);

    }
}
