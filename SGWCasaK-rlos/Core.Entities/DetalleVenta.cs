﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("DetallesVenta")]
    public class DetalleVenta : BaseEntity
    {
        public int Cantidad { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal MontoTotal { get; set; }

        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
    }
}
