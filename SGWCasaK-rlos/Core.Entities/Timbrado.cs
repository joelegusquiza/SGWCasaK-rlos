using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Timbrados")]
    public class Timbrado : BaseEntity
    {
        public int NroTimbrado { get; set; }
        public DateTimeOffset FechaInicio { get; set; }
        public DateTimeOffset FechaFin { get; set; }
        public int PuntoExpedicion { get; set; }
        public int NroInicio { get; set; }
        public int NroFin { get; set; }
        public int CajaId { get; set; }
        public Caja Caja { get; set; }

        public ICollection<Venta> Ventas { get; set; } = new HashSet<Venta>();
    }
}
