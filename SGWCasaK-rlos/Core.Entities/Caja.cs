using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Caja : BaseEntity
    {
        public string Nombre { get; set; }

        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<Timbrado> Timbrados { get; set; } = new HashSet<Timbrado>();
        public ICollection<CajaAperturaCierre> CajaAperturasCierres { get; set; } = new HashSet<CajaAperturaCierre>();
        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
    }
}
