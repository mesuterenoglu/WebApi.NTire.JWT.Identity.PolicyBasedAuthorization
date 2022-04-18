

using AutoMapper;
using BLL.IServices;
using Core.DTOs;
using Core.Interfaces;
using System;
using System.Threading.Tasks;
using Core.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        
        public async Task AddAsync(CompanyDto entity)
        {
            try
            {
                var company = _mapper.Map<Company>(entity);
                await _companyRepository.AddAsync(company);
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
                return await _companyRepository.AnyAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<Company, bool>> filter)
        {
            try
            {
                return await _companyRepository.AnyAsync(filter);
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
                await _companyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyDto>> GetAllActiveAsync()
        {
            try
            {
                var companies = await _companyRepository.GetAllActiveAsync();
                var dtos = _mapper.Map<List<CompanyDto>>(companies);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyDto>> GetAllAsync()
        {
            try
            {
                var companies = await _companyRepository.GetAllAsync();
                var dtos = _mapper.Map<List<CompanyDto>>(companies);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyDto>> GetAllInActiveAsync()
        {
            try
            {
                var companies = await _companyRepository.GetAllInActiveAsync();
                var dtos = _mapper.Map<List<CompanyDto>>(companies);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<List<CompanyDto>> GetbyFilterAsync(Expression<Func<Company, bool>> filter)
        {
            try
            {
                var companies = await _companyRepository.GetbyFilterAsync(filter);
                var dtos = _mapper.Map<List<CompanyDto>>(companies);
                return dtos;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public async Task<CompanyDto> GetbyIdAsync(Guid id)
        {
            try
            {
                var company = await _companyRepository.GetbyIdAsync(id);
                var dto = _mapper.Map<CompanyDto>(company);
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
                await _companyRepository.RemoveFromDbAsync(id);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
        public async Task UpdateAsync(CompanyDto entity)
        {
            try
            {
                var company = await _companyRepository.GetbyIdAsync(entity.Id);
                company.PhoneNumber = entity.PhoneNumber;
                company.Address = entity.Address;
                company.CompanyName = entity.CompanyName;
                await _companyRepository.UpdateAsync(company);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
