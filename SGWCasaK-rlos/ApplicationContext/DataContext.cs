using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


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

        public DataContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseSqlServer("Server=tcp:f3.database.windows.net,1433;Initial Catalog=facturas-production;Persist Security Info=False;User ID=rugertek;Password=Ketregur9097!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            return new DataContext(builder.Options, _contextAccessor, _configuration);
        }
    }
}

