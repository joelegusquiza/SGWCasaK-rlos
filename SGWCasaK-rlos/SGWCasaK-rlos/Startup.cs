using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationContext;
using Core.Automapper;
using Core.DAL.Interfaces;
using Core.DAL.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdfServices.Interfaces;
using PdfServices.Services;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Register it as scope, because it uses Repository that probably uses dbcontext
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddAuthorization(options =>
            {       
                foreach (var permission in Enum.GetValues(typeof(AccessFunctions)))
                {
                    // assuming .Permission is enum
                    options.AddPolicy(permission.ToString(),
                        policy => policy.Requirements.Add(new PermissionRequirement((AccessFunctions)permission)));
                }
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(opts =>
               {
                   opts.LoginPath = new PathString("/Shared/Login/Index");
                   opts.AccessDeniedPath = new PathString("/Shared/Login/Index");
                   opts.LogoutPath = new PathString("/Shared/Login/Index");
                   opts.SlidingExpiration = true;
               });
            
			services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
			services.AddSingleton(x => Configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IVentas, VentasServices>();
            services.AddSingleton<IProductos, ProductosServices>();
            services.AddSingleton<IClientes, ClientesService>();
            services.AddSingleton<ITimbrados, TimbradosService>();
            services.AddSingleton<ICompras, ComprasService>();
            services.AddSingleton<IProveedores, ProveedoresService>();
            services.AddSingleton<IRoles, RolesService>();
            services.AddSingleton<IUsuarios, UsuariosService>();
            services.AddSingleton<ICategoriaProductos, CategoriaProductosService>();
            services.AddSingleton<IEmailSender, EmailSenderService>();
            services.AddSingleton<IEnvironmentContext, EnvironmentContextService>();
            services.AddSingleton<IPedidos, PedidosService>();
            services.AddSingleton<IInventario, InventarioService>();
            services.AddSingleton<ICajas, CajasService>();
            services.AddSingleton<ISucursales, SucursalesService>();
            services.AddSingleton<IOrdenPagoCompras, OrdenPagoComprasService>();
            services.AddSingleton<ICajaAperturaCierre, CajaAperturaCierreService>();
			services.AddSingleton<IRecibos, RecibosService>();
			services.AddSingleton<ICuotas, CuotasService>();
			services.AddSingleton<IPdfCreation, PdfCreationServices>();
			services.AddSingleton<IReportes, ReportesService>();
			services.AddScoped<UserEmailActiveFilter>();
			
			return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<VentasProfile>();
                cfg.AddProfile<ProductosProfile>();
                cfg.AddProfile<ClientesProfile>();
                cfg.AddProfile<ProveedoresProfile>();
                cfg.AddProfile<ComprasProfile>();
                cfg.AddProfile<RolesProfile>();
                cfg.AddProfile<UsuariosProfile>();
                cfg.AddProfile<CategoriaProductosProfile>();
                cfg.AddProfile<PedidosProfile>();
                cfg.AddProfile<TimbradosProfile>();
                cfg.AddProfile<InventarioProfile>();
                cfg.AddProfile<CajasProfile>();
                cfg.AddProfile<SucursalesProfile>();
                cfg.AddProfile<OrdenPagoCompraProfile>();
                cfg.AddProfile<CajaAperturaCierreProfile>();
				cfg.AddProfile<RecibosProfile>();
				cfg.AddProfile<CuotasProfile>();
				cfg.AddProfile<DashboardProfile>();
				cfg.AddProfile<ReportesProfile>();
			});
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
