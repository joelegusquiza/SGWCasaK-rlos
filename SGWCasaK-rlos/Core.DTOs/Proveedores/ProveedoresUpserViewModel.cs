using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Proveedores
{
    public class ProveedoresUpserViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public int RUC { get; set; }
        public int DigitoVerificador { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public TiposProveedores Tipo { get; set; }

        public List<DropDownViewModel<TiposProveedores>> TiposProveedores { get; set; } = Enum.GetValues(typeof(TiposProveedores)).Cast<TiposProveedores>().Select(x => new DropDownViewModel<TiposProveedores>
        {
            Text = x.ToString(),
            Value = x
        }).ToList();


    }
}
