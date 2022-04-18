

using Core.Entities;
using Core.Interfaces;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext context) : base(context)
        {

        }
    }
}
