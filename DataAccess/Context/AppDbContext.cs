using Core.Entities;
using Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            DateTime dateTime = DateTime.Now;
            foreach (var item in modifiedEntries)
            {
                var entity = item.Entity as BaseEntity;

                if (entity != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        entity.CreatedDate = dateTime;
                        entity.IsActive = true;

                    }
                    else if (item.State == EntityState.Modified)
                    {
                        entity.ModifiedDate = dateTime;
                        entity.IsModified = true;
                    }
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
