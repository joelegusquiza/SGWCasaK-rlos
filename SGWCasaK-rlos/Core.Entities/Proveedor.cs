using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Proveedores")]
    public class Proveedor : BaseEntity
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public decimal Saldo { get; set; }

        public ICollection<Compra> Compras { get; set; } = new HashSet<Compra>();
    }
}
