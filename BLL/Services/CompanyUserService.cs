using AutoMapper;
using BLL.IServices;
using Core.DTOs;
using Core.Interfaces;
using System.Threading.Tasks;
using Core.Entities;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using Common;

namespace BLL.Services
{
    public class CompanyUserService : ICompanyUserService
    {
        private readonly ICompanyUserRepository _companyUserRepository;
        private readonly IMapper _mapper;

        public CompanyUserService(ICompanyUserRepository companyUserRepository,IMapper mapper)
        {
            _companyUserRepository = companyUserRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(CompanyUserDto entity)
        {
            try
            {
                var companyUser = _mapper.Map<CompanyUser>(entity);
                await _companyUserRepository.AddAsync(companyUser);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            try
            {
                return await _companyUserRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<CompanyUser, bool>> filter)
        {
            try
            {
                return await _companyUserRepository.AnyAsync(filter);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _companyUserRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyUserDto>> GetAllActiveAsync()
        {
            try
            {
                var companyUsers = await _companyUserRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<CompanyUserDto>>(companyUsers);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyUserDto>> GetAllAsync()
        {
            try
            {
                var companyUsers = await _companyUserRepository.GetAllAsync();
                var dtos = _mapper.Map<List<CompanyUserDto>>(companyUsers);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyUserDto>> GetAllInActiveAsync()
        {
            try
            {
                var companyUsers = await _companyUserRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<CompanyUserDto>>(companyUsers);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyUserDto>> GetbyFilterAsync(Expression<Func<CompanyUser, bool>> filter)
        {
            try
            {
                var companyUsers = await _companyUserRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<CompanyUserDto>>(companyUsers);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CompanyUserDto> GetbyIdAsync(Guid id)
        {
            try
            {
                var companyUser = await _companyUserRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<CompanyUserDto>(companyUser);
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CompanyUserDto> GetCompanyUserbyAppUserIdAsync(string appUserId)
        {
            try
            {
                var companyUser = await _companyUserRepository.GetCompanyUserbyAppUserIdAsync(appUserId);
                var dto = _mapper.Map<CompanyUserDto>(companyUser);
                return dto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task RemoveFromDbAsync(Guid id)
        {
            try
            {
                await _companyUserRepository.RemoveFromDbAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task UpdateAsync(CompanyUserDto entity)
        {
            try
            {
                var companyUser = await _companyUserRepository.GetbyIdAsync(entity.Id);
                companyUser.FirstName = entity.FirstName;
                companyUser.LastName = entity.LastName;
                companyUser.CompanyId = entity.CompanyId;
                await _companyUserRepository.UpdateAsync(companyUser);

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
