using BLL.DIContainer;
using BLL.IServices;
using Core.DTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x=>x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });

            services.AddDIContainer(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            CreateAdmin(provider);
            CreateRoles(provider);
        }

        private async void CreateAdmin(IServiceProvider provider)
        {
            var userService = provider.GetRequiredService<IUserService>();
            var roleService = provider.GetRequiredService<IRoleService>();

            var resultRole = await roleService.RoleExistAsync("Admin");

            var user = new AppUserDto
            {
                Id = Guid.NewGuid().ToString(),
                Email = Configuration["UserSettings:Mail"]
            };

            var resultUser = await userService.AnybyEmailAsync(user.Email);

            if (!resultRole && !resultUser)
            {
                await roleService.CreateRoleAsync("Admin");
                await userService.RegisterAsync(user, Configuration["UserSettings:Password"]);
                await roleService.AssignRoleAsync(user.Email, "Admin");
            }
            else
            {
                var result = await roleService.IsInRoleAsync(user.Email, "Admin");
                if (!result)
                {
                    await roleService.AssignRoleAsync(user.Email, "Admin");
                }
            }
        }

        private async void CreateRoles(IServiceProvider provider)
        {
            var roleService = provider.GetRequiredService<IRoleService>();
            var resultCO = await roleService.RoleExistAsync("CompanyOwner");
            if (!resultCO)
            {
                await roleService.CreateRoleAsync("CompanyOwner");
            }
        }
    }
}
