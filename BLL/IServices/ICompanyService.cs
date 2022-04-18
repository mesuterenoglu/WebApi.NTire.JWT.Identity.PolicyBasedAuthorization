

using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ICompanyService
    {
        Task AddAsync(CompanyDto entity);
        Task DeleteAsync(Guid id);
        Task RemoveFromDbAsync(Guid id);
        Task UpdateAsync(CompanyDto entity);
        Task<List<CompanyDto>> GetAllAsync();
        Task<List<CompanyDto>> GetAllInActiveAsync();
        Task<List<CompanyDto>> GetAllActiveAsync();
        Task<CompanyDto> GetbyIdAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<Company, bool>> filter);
        Task<List<CompanyDto>> GetbyFilterAsync(Expression<Func<Company, bool>> filter);
    }
}
