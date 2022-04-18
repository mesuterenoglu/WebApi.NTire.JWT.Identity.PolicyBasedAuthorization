using Core.DTOs;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BLL.IServices
{
    public interface ICompanyUserService
    {
        Task AddAsync(CompanyUserDto entity);
        Task DeleteAsync(Guid id);
        Task RemoveFromDbAsync(Guid id);
        Task UpdateAsync(CompanyUserDto entity);
        Task<List<CompanyUserDto>> GetAllAsync();
        Task<List<CompanyUserDto>> GetAllInActiveAsync();
        Task<List<CompanyUserDto>> GetAllActiveAsync();
        Task<CompanyUserDto> GetbyIdAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<CompanyUser, bool>> filter);
        Task<List<CompanyUserDto>> GetbyFilterAsync(Expression<Func<CompanyUser, bool>> filter);
        Task<CompanyUserDto> GetCompanyUserbyAppUserIdAsync(string appUserId);
        Task<CompanyUserDto> GetCompanyUserbyAppUserEmailAsync(string appUserEmail);
    }
}
