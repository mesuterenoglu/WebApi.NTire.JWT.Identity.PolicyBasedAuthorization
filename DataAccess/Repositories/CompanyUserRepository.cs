using Core.Entities;
using Core.Interfaces;
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class CompanyUserRepository : BaseRepository<CompanyUser>,ICompanyUserRepository
    {
        public CompanyUserRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<CompanyUser> GetCompanyUserbyAppUserIdAsync(string appUserId)
        {
            var companyUser = await _context.CompanyUsers.FirstOrDefaultAsync(x => x.AppUserId == appUserId);
            return companyUser;
        }
    }
}
