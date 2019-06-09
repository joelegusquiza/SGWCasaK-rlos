using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    public class Inventario : BaseEntity
    {
        public InventarioEstado Estado { get; set; }
		public string RazonAnulado { get; set; }

        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
		public int UsuarioInicioId { get; set; }
		public Usuario UsuarioInicio { get; set; }
		public int? UsuarioFinId { get; set; }
		public Usuario UsuarioFin { get; set; }
        public ICollection<DetalleInventario> DetalleInventario { get; set; } = new HashSet<DetalleInventario>();
    }
}
