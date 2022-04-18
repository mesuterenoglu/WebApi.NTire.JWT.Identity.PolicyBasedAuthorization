
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Policies.CompanyUserPolicy
{
    public class SameCompanyRequirementHandler : AuthorizationHandler<SameCompanyRequirement>
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyRepository _companyRepository;

        public SameCompanyRequirementHandler(IHttpContextAccessor httpContext, UserManager<AppUser> userManager, ICompanyRepository companyRepository)
        {
            _httpContext = httpContext;
            _userManager = userManager;
            _companyRepository = companyRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameCompanyRequirement requirement)
        {
            try
            {
                var path = _httpContext.HttpContext.Request.Path.Value;

                var pathValues = path.Split("/");

                var companyId = pathValues[pathValues.Length - 1];

                var company = _companyRepository.GetbyIdAsync(new Guid(companyId)).Result;

                if (company == null)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
                var userId = context.User.Claims.Where(c => c.Type == "UserId").First().Value;

                var userIds = company.CompanyUsers.Where(x => x.AppUserId == userId).Select(x => x.AppUserId).ToList();

                if (userIds == null || !userIds.Contains(userId))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                context.Succeed(requirement);

                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw new InvalidOperationException(Messages.Unauthorized);
            }
        }
    }
}
