using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Sucursal : BaseEntity
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CodigoEstablecimiento { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<Timbrado> Timbrados { get; set; } = new HashSet<Timbrado>();
        public ICollection<Caja> Cajas { get; set; } = new HashSet<Caja>();
        
    }
}
