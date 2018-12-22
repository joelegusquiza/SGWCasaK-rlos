﻿// <auto-generated />
using System;
using ApplicationContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationContext.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20181220020043_configureDB")]
    partial class configureDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("Core.Entities.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<decimal>("Excenta");

                    b.Property<decimal>("FechaCompra");

                    b.Property<decimal>("IvaCinco");

                    b.Property<decimal>("IvaDiez");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("ProveedorId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

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

                    b.Property<decimal>("PrecioCompra");

                    b.Property<int>("ProductoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("DetallesCompra");
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

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("PedidoId");

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

                    b.Property<decimal>("MontoTotal");

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

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("DireccionString");

                    b.Property<float>("Latitud");

                    b.Property<float>("Longitud");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Direccion");
                });

            modelBuilder.Entity("Core.Entities.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<bool>("Delivery");

                    b.Property<string>("DireccionEntrega");

                    b.Property<DateTimeOffset>("FechaEntrega");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

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

                    b.Property<decimal>("PrecioVenta");

                    b.Property<int>("Stock");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaProductoId");

                    b.ToTable("Productos");
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

                    b.Property<string>("Nombre");

                    b.Property<string>("RUC");

                    b.Property<string>("RazonSocial");

                    b.Property<decimal>("Saldo");

                    b.Property<string>("Telefono");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Core.Entities.Timbrado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<DateTimeOffset>("FechaFin");

                    b.Property<DateTimeOffset>("FechaInicio");

                    b.Property<int>("NroFin");

                    b.Property<int>("NroInicio");

                    b.Property<int>("NroTimbrado");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Timbrados");
                });

            modelBuilder.Entity("Core.Entities.UnidadMedida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Nombre");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("UnidadesMedida");
                });

            modelBuilder.Entity("Core.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Apellido");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("Expiration");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Nombre");

                    b.Property<string>("PasswordHash");

                    b.Property<decimal>("Saldo");

                    b.Property<string>("Salt");

                    b.Property<string>("Telefono");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Core.Entities.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateModified");

                    b.Property<decimal>("Exenta");

                    b.Property<decimal>("IvaCinco");

                    b.Property<decimal>("IvaDiez");

                    b.Property<decimal>("MontoTotal");

                    b.Property<int>("NroFactura");

                    b.Property<int>("TimbradoId");

                    b.Property<int>("UserCreatedId");

                    b.Property<int>("UserModifiedId");

                    b.HasKey("Id");

                    b.HasIndex("TimbradoId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Core.Entities.Compra", b =>
                {
                    b.HasOne("Core.Entities.Proveedor", "Proveedor")
                        .WithMany("Compras")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Restrict);
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
                    b.HasOne("Core.Entities.Usuario", "Usuario")
                        .WithMany("Direcciones")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Producto", b =>
                {
                    b.HasOne("Core.Entities.CategoriaProducto", "CategoriaProducto")
                        .WithMany("Productos")
                        .HasForeignKey("CategoriaProductoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Core.Entities.Venta", b =>
                {
                    b.HasOne("Core.Entities.Timbrado", "Timbrado")
                        .WithMany("Ventas")
                        .HasForeignKey("TimbradoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}