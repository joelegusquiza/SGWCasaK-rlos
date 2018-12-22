using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities
{
    [Table("UnidadesMedida")]
    public class UnidadMedida : BaseEntity
    {
        public string Nombre { get; set; }

    }
}
