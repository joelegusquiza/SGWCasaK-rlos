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
            [Description("Agregar Compra")]
            AddCompra = 2300,
            [Description("Editar Compra")]
            EditCompra = 2400,
            [Description("Eliminar Compra")]
            DeleteCompra = 2500,
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
            [Description("Anular Venta")]
            AnularVenta = 4100,
            [Description("Ver Reportes")]
            VerReportes = 4200,
            [Description("Ver Inventarios")]
            IndexInventario = 4300,
            [Description("Agregar Inventario")]
            AddInventario = 4400,                        
            [Description("Eliminar Inventario")]
            DeleteInventario = 4500,
            [Description("Ver Detalle Inventario")]
            ViewInventario = 4600,
        }
        
        public enum EstadoVenta
        {
            Pagado,
            Anulado,
            Pendiente
        }

        public enum EstadoCompra
        {
            Pagado,
            Anulado,
            Pendiente
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

        public static class CustomClaimTypes
        {
            public static string UserId = "UserId";
        }
    }
}
