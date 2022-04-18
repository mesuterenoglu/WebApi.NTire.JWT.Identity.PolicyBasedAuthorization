

using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICompanyUserRepository : IRepository<CompanyUser>
    {
        Task<CompanyUser> GetCompanyUserbyAppUserIdAsync(string appUserId);
    }
}
