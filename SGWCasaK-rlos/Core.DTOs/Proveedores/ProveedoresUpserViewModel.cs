using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Proveedores
{
    public class ProveedoresUpserViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        
    }
}
