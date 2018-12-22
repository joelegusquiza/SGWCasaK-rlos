using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("Direccion")]
    public class Direccion : BaseEntity
    {
        public string DireccionString { get; set; }
        public float Latitud { get; set; }
        public float Longitud { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
