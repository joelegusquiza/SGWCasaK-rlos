using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Core
{
    public class Constants
    {
        public enum AccessFunctions
        {
            [Description("Ver Roles")]
            IndexRole = 99,
            [Description("Agregar Rol")]
            AddRole = 100,
            [Description("Editar Rol")]
            EditRole = 200,
            [Description("Eliminar Rol")]
            DeleteRole = 300,
            [Description("Ver Usuarios")]
            IndexUsuario = 399,
            [Description("Agregar Usuario")]
            AddUsuario = 400,
            [Description("Editar Usuario")]
            EditUsuario = 500,
            [Description("Eliminar Usuario")]
            DeleteUsuario = 600,
            [Description("Ver Pedidos")]
            IndexPedido = 700,
            [Description("Agregar Pedido")]
            AddPedido = 799,
            [Description("Editar Pedido")]
            EditPedido = 800,
            [Description("Eliminar Pedido")]
            DeletePedido = 900,
            [Description("Generar Venta del Pedido")]
            GenerateVentaPedido = 901,
            [Description("Cambiar Estado del Pedido")]
            ChangeEstadoPedido = 902,
            [Description("Cliente Ver Pedido")]
            ClienteIndexPedido = 1000,
            [Description("Cliente Agregar Pedido")]
            ClienteAddPedido = 1100,
            [Description("Cliente Editar Pedido")]
            ClienteEditPedido = 1200,
            [Description("Cliente Eliminar Pedido")]
            ClienteDeletePedido = 1300,
            [Description("Ver Caregoria")]
            IndexCategoria = 1400,
            [Description("Agregar Categoria")]
            AddCategoria = 1500,
            [Description("Editar Categoria")]
            EditCategoria = 1600,
            [Description("Eliminar Categoria")]
            DeleteCategoria = 1700,
            [Description("Ver Clientes")]
            IndexCliente = 1800,
            [Description("Agregar Cliente")]
            AddCliente = 1900,
            [Description("Editar Cliente")]
            EditCliente = 2000,
            [Description("Eliminar Cliente")]
            DeleteCliente = 2100,
            [Description("Ver Compras")]
            IndexCompra = 2200,
            [Description("Ver Compras Pendientes")]
            IndexComprasPending = 2250,
            [Description("Agregar Compra")]
            AddCompra = 2300,
            [Description("Editar Compra")]
            EditCompra = 2400,
            [Description("Anular Compra")]
            AnularCompra = 2500,
            [Description("Confirmar Compra")]
            ConfirmCompra = 2550,
            [Description("Ver Productos")]
            IndexProducto = 2600,
            [Description("Agregar Producto")]
            AddProducto = 2700,
            [Description("Editar Producto")]
            EditProducto = 2800,
            [Description("Eliminar Producto")]
            DeleteProducto = 2900,
            [Description("Ver Proveedores")]
            IndexProveedor = 3000,
            [Description("Agregar Proveedor")]
            AddProveedor = 3100,
            [Description("Editar Proveedor")]
            EditProveedor = 3200,
            [Description("Eliminar Provedor")]
            DeleteProveedor = 3300,
            [Description("Ver Timbrados")]
            IndexTimbrado = 3400,
            [Description("Agregar Timbrados")]
            AddTimbrado = 3500,
            [Description("Editar Timbrados")]
            EditTimbrado = 3600,
            [Description("Eliminar Timbrados")]
            DeleteTimbrado = 3700,
            [Description("Ver Ventas")]
            IndexVenta = 3800,
            [Description("Agregar Venta")]
            AddVenta = 3900,
            [Description("Editar Venta")]
            EditVenta = 4000,
			[Description("Confirmar Venta")]
			ConfirmarVenta = 4050,
			[Description("Anular Venta")]
            AnularVenta = 4100,
            [Description("Ver Reportes")]
            VerReportes = 4200,
            [Description("Ver Inventarios")]
            IndexInventario = 4300,
            [Description("Agregar, editar Inventario")]
            Upsertnventario = 4400,                        
            [Description("Confirmar Inventario")]
            ConfirmInventario = 4500,
            [Description("Anular Inventario")]
            AnularInventario = 4600,
            [Description("Ver Cajas")]
            IndexCaja = 4700,
            [Description("Agregar Cajas")]
            AddCaja = 4800,
            [Description("Editar Cajas")]
            EditCaja = 4900,
            [Description("Eliminar Cajas")]
            DeleteCaja = 5000,
            [Description("Ver Sucursales")]
            IndexSucursal = 5100,
            [Description("Agregar Sucursales")]
            AddSucursal = 5200,
            [Description("Editar Sucursales")]
            EditSucursal = 5300,
            [Description("Eliminar Sucursales")]
            DeleteSucursal = 5400,
            [Description("Cambiar de Sucursal")]
            ChangeSucursal = 5500,
            [Description("Ver Orden de Pago de Compras")]
            IndexOrdenPagoCompras = 5600,
            [Description("Agregar Orden de Pago de Compras")]
            AddOrdenPagoCompras = 5700,
            [Description("Editar Orden de pago de Compras")]
            EditOrdenPagoCompras = 5800,
            [Description("Anular Orden de pago de compras")]
            AnularOrdenPagoCompras = 5900,
            [Description("Ver Apertura-Cierre de Caja")]
            IndexAperturaCierreCaja = 6000,
			[Description("Ver Recibo")]
			IndexRecibo = 6100,
			[Description("Agregar Recibo")]
			AddRecibo = 6200,
			[Description("Editar Recibo")]
			EditRecibo = 6300,
			[Description("Eliminar Recibo")]
			DeleteRecibo = 6400,
		}
        
        public enum EstadoVenta
        {
            Pendiente,
            Confirmado,
			PendientedePago,
            Pagado,
            Anulado            
        }

        public enum EstadoCompra
        {
            Pendiente,
            Confirmado,
            PendientedePago,
            Pagado,
            Anulado,
        }   

		public enum EstadoCuota 
		{ 
			Pendiente,
			Pagado
		}

		public enum EstadoRecibo
		{
			Pendiente,
			Pagado
		}
        
        public enum EstadoPedido
        { 
            [Description("Pendiente")]
            Pendiente,
            [Description("En Proceso")]
            EnProceso,
            [Description("Preparado")]
            Preparado,
            [Description("Finalizado")]
            Finalizado
        }

        public enum PorcIva
        {
            Excento = 1,
            Cinco = 2,
            Diez = 3
        }

        public enum CondicionVenta
        {
            Contado,
            Credito
        }

        public enum CondicionCompra
        {
            Contado,
            Credito
        }

        public enum UnidadesMedida
        {
            kilogramo,
            gramo,
            litro,
            centimetro,
            metro,
            metroscuadrados,
            metroscubicos,
            unidad
        }

        public enum TiposProveedores
        {
            Fisica,
            Juridica
        }

        public enum OrdenPagoCompraEstado
        {
            Pendiente,
            Pagado,
            Anulado
        }

        public enum CajaTipoOperacion
        {
            Apertura,
            Cierre
        }

        public enum InventarioEstado
        { 
            Pendiente,
            Terminado,
            Confirmado,
            Anulado
        }


        public static class CustomClaimTypes
        {
            public static string UserId = "UserId";
        }
    }
}
