﻿// <auto-generated />
using System;
using ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationContext.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Core.Entities.Caja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Nombre");

                    b.Property<int>("SucursalId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("SucursalId");

                    b.ToTable("Cajas");
                });

            modelBuilder.Entity("Core.Entities.CajaAperturaCierre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CajaId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTimeOffset?>("FechaApertura");

                    b.Property<DateTimeOffset?>("FechaCierre");

                    b.Property<decimal>("MontoApertura");

                    b.Property<decimal>("MontoCierre");

                    b.Property<int>("Tipo");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("CajaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("CajaAperturaCierre");
                });

            modelBuilder.Entity("Core.Entities.CategoriaProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("CategoriaProductos");
                });

            modelBuilder.Entity("Core.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Apellido");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Nombre");

                    b.Property<string>("RazonSocial");

                    b.Property<string>("Ruc");

                    b.Property<decimal>("Saldo");

                    b.Property<string>("Telefono");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Core.Entities.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<decimal>("Cambio");

                    b.Property<int>("CondicionCompra");

                    b.Property<DateTimeOffset>("DateCompra");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("Estado");

                    b.Property<decimal>("Excenta");

                    b.Property<DateTimeOffset>("FechaFin");

                    b.Property<DateTimeOffset>("FechaInicio");

                    b.Property<decimal>("IvaCinco");

                    b.Property<decimal>("IvaDiez");

                    b.Property<decimal>("MontoTotal");

                    b.Property<string>("NroFactura");

                    b.Property<int?>("ProveedorId");

                    b.Property<string>("RazonAnulado");

                    b.Property<int>("SucursalId");

                    b.Property<string>("Timbrado");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Core.Entities.DetalleCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("Cantidad");

                    b.Property<int>("CompraId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<int>("Equivalencia");

                    b.Property<decimal>("MontoTotal");

                    b.Property<decimal>("PrecioCompra");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesCompra");
                });

            modelBuilder.Entity("Core.Entities.DetalleInventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("InventarioId");

                    b.Property<int>("ProductoId");

                    b.Property<int>("StockActual");

                    b.Property<int>("StockEncontrado");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("InventarioId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetalleInventario");
                });

            modelBuilder.Entity("Core.Entities.DetallePedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("Cantidad");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<int>("Equivalencia");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("PedidoId");

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("PedidoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesPedido");
                });

            modelBuilder.Entity("Core.Entities.DetalleVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("Cantidad");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<int>("Equivalencia");

                    b.Property<decimal>("MontoTotal");

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.Property<int>("VentaId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("DetallesVenta");
                });

            modelBuilder.Entity("Core.Entities.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("DireccionString");

                    b.Property<float>("Latitud");

                    b.Property<float>("Longitud");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Direccion");
                });

            modelBuilder.Entity("Core.Entities.Inventario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("Estado");

                    b.Property<int>("SucursalId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("SucursalId");

                    b.ToTable("Inventario");
                });

            modelBuilder.Entity("Core.Entities.OrdenPagoCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("Estado");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("ProveedorId");

                    b.Property<int>("SucursalId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.HasIndex("SucursalId");

                    b.ToTable("OrdenPagoCompra");
                });

            modelBuilder.Entity("Core.Entities.OrdenPagoCompraDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CompraId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<decimal>("Monto");

                    b.Property<int>("OrdenPagoCompraId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("OrdenPagoCompraId");

                    b.ToTable("OrdenPagoCompraDetalle");
                });

            modelBuilder.Entity("Core.Entities.PagoVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<decimal>("Monto");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.Property<int>("VentaId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VentaId");

                    b.ToTable("PagosVentas");
                });

            modelBuilder.Entity("Core.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<bool>("Delivery");

                    b.Property<string>("DireccionEntrega");

                    b.Property<int>("Estado");

                    b.Property<DateTimeOffset?>("FechaEntrega");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Core.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CategoriaProductoId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.Property<double>("PorcentajeGanancia");

                    b.Property<int>("PorcentajeIva");

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("UnidadMedida");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaProductoId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Core.Entities.ProductoPresentacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<int>("Equivalencia");

                    b.Property<string>("Nombre");

                    b.Property<double>("PorcentajeGanancia");

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("ProductoPresentaciones");
                });

            modelBuilder.Entity("Core.Entities.ProductoSucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("ProductoId");

                    b.Property<int>("Stock");

                    b.Property<int>("SucursalId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("SucursalId");

                    b.ToTable("ProductoSucursal");
                });

            modelBuilder.Entity("Core.Entities.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Apellido");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Direccion");

                    b.Property<string>("Email");

                    b.Property<string>("Nombre");

                    b.Property<string>("RUC");

                    b.Property<string>("RazonSocial");

                    b.Property<decimal>("Saldo");

                    b.Property<string>("Telefono");

                    b.Property<int>("Tipo");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Core.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Descripcion");

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsCajero");

                    b.Property<bool>("IsCliente");

                    b.Property<string>("Nombre");

                    b.Property<string>("Permisos");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Core.Entities.Sucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CodigoEstablecimiento");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Direccion");

                    b.Property<string>("Nombre");

                    b.Property<string>("Telefono");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Sucursales");
                });

            modelBuilder.Entity("Core.Entities.Timbrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CajaId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTimeOffset>("FechaFin");

                    b.Property<DateTimeOffset>("FechaInicio");

                    b.Property<int>("NroFin");

                    b.Property<int>("NroInicio");

                    b.Property<int>("NroTimbrado");

                    b.Property<int>("PuntoExpedicion");

                    b.Property<int>("SucursalId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CajaId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Timbrados");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Apellido");

                    b.Property<int?>("CajaId");

                    b.Property<int?>("ClienteId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<bool>("EmailVerified");

                    b.Property<DateTime?>("Expiration");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Nombre");

                    b.Property<string>("PasswordHash");

                    b.Property<int>("RolId");

                    b.Property<string>("Salt");

                    b.Property<int?>("SucursalId");

                    b.Property<string>("Telefono");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.Property<Guid>("UserVerifyEmailGuid");

                    b.HasKey("Id");

                    b.HasIndex("CajaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("RolId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Core.Entities.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("CajaId");

                    b.Property<decimal>("Cambio");

                    b.Property<int>("ClienteId");

                    b.Property<int>("CondicionVenta");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<int>("Estado");

                    b.Property<decimal>("Excenta");

                    b.Property<decimal>("IvaCinco");

                    b.Property<decimal>("IvaDiez");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("NroFactura");

                    b.Property<int?>("PedidoId");

                    b.Property<int>("SucursalId");

                    b.Property<int>("TimbradoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CajaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PedidoId");

                    b.HasIndex("SucursalId");

                    b.HasIndex("TimbradoId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Core.Entities.Caja", b =>
                {
                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Cajas")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.CajaAperturaCierre", b =>
                {
                    b.HasOne("Core.Entities.Caja", "Caja")
                        .WithMany("CajaAperturasCierres")
                        .HasForeignKey("CajaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Usuario", "Usuario")
                        .WithMany("CajaAperturasCierres")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Compra", b =>
                {
                    b.HasOne("Core.Entities.Proveedor", "Proveedor")
                        .WithMany("Compras")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Compras")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Core.Entities.DetalleCompra", b =>
                {
                    b.HasOne("Core.Entities.Compra", "Compra")
                        .WithMany("DetalleCompra")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("DetalleCompra")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.DetalleInventario", b =>
                {
                    b.HasOne("Core.Entities.Inventario", "Inventario")
                        .WithMany("DetalleInventario")
                        .HasForeignKey("InventarioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("DetalleInventario")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.DetallePedido", b =>
                {
                    b.HasOne("Core.Entities.Pedido", "Pedido")
                        .WithMany("DetallePedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("DetallePedido")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.DetalleVenta", b =>
                {
                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Venta", "Venta")
                        .WithMany("DetalleVenta")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Direccion", b =>
                {
                    b.HasOne("Core.Entities.Cliente", "Cliente")
                        .WithMany("Direcciones")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Inventario", b =>
                {
                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Inventarios")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.OrdenPagoCompra", b =>
                {
                    b.HasOne("Core.Entities.Proveedor", "Proveedor")
                        .WithMany("PagosCompra")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("OrdenesPagoCompra")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.OrdenPagoCompraDetalle", b =>
                {
                    b.HasOne("Core.Entities.Compra", "Compra")
                        .WithMany("OrdenPagoDetalle")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.OrdenPagoCompra", "OrdenPagoCompra")
                        .WithMany("OrdenPagoDetalle")
                        .HasForeignKey("OrdenPagoCompraId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.PagoVenta", b =>
                {
                    b.HasOne("Core.Entities.Cliente", "Cliente")
                        .WithMany("PagosVenta")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Venta", "Venta")
                        .WithMany("PagosVenta")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Pedido", b =>
                {
                    b.HasOne("Core.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Producto", b =>
                {
                    b.HasOne("Core.Entities.CategoriaProducto", "CategoriaProducto")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.ProductoPresentacion", b =>
                {
                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("ProductoPresentaciones")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.ProductoSucursal", b =>
                {
                    b.HasOne("Core.Entities.Producto", "Producto")
                        .WithMany("ProductoSucursal")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("ProductoSucursal")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Timbrado", b =>
                {
                    b.HasOne("Core.Entities.Caja", "Caja")
                        .WithMany("Timbrados")
                        .HasForeignKey("CajaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Timbrados")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.HasOne("Core.Entities.Caja")
                        .WithMany("Usuarios")
                        .HasForeignKey("CajaId");

                    b.HasOne("Core.Entities.Cliente", "Cliente")
                        .WithMany("Usuarios")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Usuarios")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Venta", b =>
                {
                    b.HasOne("Core.Entities.Caja", "Caja")
                        .WithMany("Ventas")
                        .HasForeignKey("CajaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Cliente", "Cliente")
                        .WithMany("Ventas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Pedido", "Pedido")
                        .WithMany("Ventas")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Core.Entities.Sucursal", "Sucursal")
                        .WithMany("Ventas")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Core.Entities.Timbrado", "Timbrado")
                        .WithMany("Ventas")
                        .HasForeignKey("TimbradoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
