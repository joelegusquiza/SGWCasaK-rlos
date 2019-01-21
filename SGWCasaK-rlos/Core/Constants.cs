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
            [Description("Agregar Rol")]
            AddRole = 100,
            [Description("Editar Rol")]
            EditRole = 200,
            [Description("Eliminar Rol")]
            DeleteRole = 300,
            [Description("Agregar Usuario")]
            AddUsuario = 400,
            [Description("Editar Usuario")]
            EditUsuario = 500,
            [Description("Eliminar Usuario")]
            DeleteUsuario = 600,
            [Description("Agregar Pedido")]
            AddPedido = 700,
            [Description("Editar Pedido")]
            EditPedido = 800,
            [Description("Eliminar Pedido")]
            DeletePedido = 900,

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
