using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CategoriaProducto> CategoriasProducto { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<DetalleCompra> DetallesCompra { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Timbrado> Timbrados { get; set; }
        public DbSet<PagoVenta> PagosVentas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<ProductoPresentacion> ProductoPresentaciones { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<DetalleInventario> DetalleInventario { get; set; }
        public DbSet<Caja> Cajas { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venta>()
                .HasMany(x => x.DetalleVenta)
                .WithOne(x => x.Venta)
                .HasForeignKey(x => x.VentaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.DetalleVenta)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
                .HasMany(x => x.DetalleCompra)
                .WithOne(x => x.Compra)
                .HasForeignKey(x => x.CompraId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.DetalleCompra)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
                .HasMany(x => x.DetallePedido)
                .WithOne(x => x.Pedido)
                .HasForeignKey(x => x.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
               .HasMany(x => x.DetallePedido)
               .WithOne(x => x.Producto)
               .HasForeignKey(x => x.ProductoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoriaProducto>()
               .HasMany(x => x.Productos)
               .WithOne(x => x.CategoriaProducto)
               .HasForeignKey(x => x.CategoriaProductoId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
               .HasMany(x => x.Direcciones)
               .WithOne(x => x.Cliente)
               .HasForeignKey(x => x.ClienteId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Proveedor>()
               .HasMany(x => x.Compras)
               .WithOne(x => x.Proveedor)
               .HasForeignKey(x => x.ProveedorId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Timbrado>()
              .HasMany(x => x.Ventas)
              .WithOne(x => x.Timbrado)
              .HasForeignKey(x => x.TimbradoId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
             .HasMany(x => x.Usuarios)
             .WithOne(x => x.Cliente)
             .HasForeignKey(x => x.ClienteId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
               .HasMany(x => x.Ventas)
               .WithOne(x => x.Cliente)
               .HasForeignKey(x => x.ClienteId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasMany(x => x.PagosVenta)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Venta>()
                .HasMany(x => x.PagosVenta)
                .WithOne(x => x.Venta)
                .HasForeignKey(x => x.VentaId)
                .OnDelete(DeleteBehavior.Restrict);           

            modelBuilder.Entity<Rol>()
                .HasMany(x => x.Usuarios)
                .WithOne(x => x.Rol)
                .HasForeignKey(x => x.RolId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.ProductoPresentaciones)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
               .HasMany(x => x.Pedidos)
               .WithOne(x => x.Cliente)
               .HasForeignKey(x => x.ClienteId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pedido>()
              .HasMany(x => x.Ventas)
              .WithOne(x => x.Pedido)
              .HasForeignKey(x => x.PedidoId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Inventario>()
                .HasMany(x => x.DetalleInventario)
                .WithOne(x => x.Inventario)
                .HasForeignKey(x => x.InventarioId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.DetalleInventario)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caja>()
                  .HasMany(x => x.Timbrados)
                  .WithOne(x => x.Caja)
                  .HasForeignKey(x => x.CajaId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
                .HasMany(x => x.Timbrados)
                .WithOne(x => x.Sucursal)
                .HasForeignKey(x => x.SucursalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
               .HasMany(x => x.Cajas)
               .WithOne(x => x.Sucursal)
               .HasForeignKey(x => x.SucursalId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
              .HasMany(x => x.Usuarios)
              .WithOne(x => x.Sucursal)
              .HasForeignKey(x => x.SucursalId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrdenPagoCompra>()
              .HasMany(x => x.OrdenPagoDetalle)
              .WithOne(x => x.OrdenPagoCompra)
              .HasForeignKey(x => x.OrdenPagoCompraId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Compra>()
               .HasMany(x => x.OrdenPagoDetalle)
               .WithOne(x => x.Compra)
               .HasForeignKey(x => x.CompraId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
                .HasMany(x => x.OrdenesPagoCompra)
                .WithOne(x => x.Sucursal)
                .HasForeignKey(x => x.SucursalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sucursal>()
                .HasMany(x => x.ProductoSucursal)
                .WithOne(x => x.Sucursal)
                .HasForeignKey(x => x.SucursalId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Producto>()
                .HasMany(x => x.ProductoSucursal)
                .WithOne(x => x.Producto)
                .HasForeignKey(x => x.ProductoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>()
               .HasMany(x => x.CajaAperturasCierres)
               .WithOne(x => x.Usuario)
               .HasForeignKey(x => x.UsuarioId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caja>()
               .HasMany(x => x.CajaAperturasCierres)
               .WithOne(x => x.Caja)
               .HasForeignKey(x => x.CajaId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Caja>()
                 .HasMany(x => x.Ventas)
                 .WithOne(x => x.Caja)
                 .HasForeignKey(x => x.CajaId)
                 .OnDelete(DeleteBehavior.Restrict);
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

