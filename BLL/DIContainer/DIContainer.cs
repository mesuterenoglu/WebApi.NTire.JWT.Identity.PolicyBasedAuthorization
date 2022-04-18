using BLL.IServices;
using BLL.Mapping;
using BLL.Services;
using Common.Policies.CompanyUserPolicy;
using Core.Entities;
using Core.Entities.Abstract;
using Core.Interfaces;
using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BLL.DIContainer
{
    public static class DIContainer
    {
        public static IServiceCollection AddDIContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(x =>
                x.UseLazyLoadingProxies().UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("DataAccess")));

            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;

                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("SameCompany", policy => policy.Requirements.Add(new SameCompanyRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, SameCompanyRequirementHandler>();

            services.AddHttpContextAccessor();

            services.AddScoped<IRepository<BaseEntity>,BaseRepository<BaseEntity>>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();

            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyUserService, CompanyUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();


            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }
    }
}
