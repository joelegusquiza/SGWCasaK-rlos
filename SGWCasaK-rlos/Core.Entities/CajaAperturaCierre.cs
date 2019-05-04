using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.Entities
{
    public class CajaAperturaCierre : BaseEntity
    {

        public DateTimeOffset? FechaApertura { get; set; }
        public DateTimeOffset? FechaCierre { get; set; }
        public decimal MontoApertura { get; set; }
        public decimal MontoCierre { get; set; }
        public CajaTipoOperacion Tipo { get; set; }

        public int CajaId { get; set; }
        public Caja Caja { get; set; }
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; }
    }
}
