using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ProductoSucursal : BaseEntity
    {
        public int Cantidad { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
