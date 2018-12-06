using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using static Core.Constants;

namespace ApplicationContext
{
    public class DataContext : DbContext, IAppContext, IDesignTimeDbContextFactory<DataContext>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfigurationRoot _configuration;
        

        public DataContext()
        {

        }

        public DataContext(DbContextOptions options, IHttpContextAccessor contextAccessor, IConfigurationRoot configuration) : base(options)
        {
            //Uncomment this lines if youd like to debbug the migration scripts
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            _contextAccessor = contextAccessor;
            _configuration = configuration;
         

        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;
            var userId = GetUserId();

            //Populate Created/ Modified By fields
            var castedList = ChangeTracker.Entries<BaseEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();
            foreach (var x in castedList)
            {
                if (x.State == EntityState.Added)
                {
                    x.Entity.DateCreated = now;
                    x.Entity.UserCreatedId = userId;
                }
                else if (x.State == EntityState.Modified)
                {
                    x.Entity.DateModified = now;
                    x.Entity.UserModifiedId = userId;
                }
            }
            return base.SaveChanges();
        }

        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer("Server=(local)\\SQLEXPRESS;Database=CasaK-rlosDev;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new DataContext(builder.Options, _contextAccessor, _configuration);
        }

        private int GetUserId()
        {
            return _contextAccessor == null ? 0 : Convert.ToInt32(_contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.UserId)?.Value ?? "0");
        }
    }
}

