
using Microsoft.AspNetCore.Authorization;

namespace Common.Policies.CompanyUserPolicy
{
    public class SameCompanyRequirement : IAuthorizationRequirement
    {
        public SameCompanyRequirement()
        {

        }
    }
}
