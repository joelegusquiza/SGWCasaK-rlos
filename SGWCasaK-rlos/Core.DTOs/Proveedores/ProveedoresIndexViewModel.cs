using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Proveedores
{
    public class ProveedoresIndexViewModel
    {
        public List<ProveedorViewModel> Proveedores { get; set; } = new List<ProveedorViewModel>();
    }

    public class ProveedorViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(RazonSocial))
                    return RazonSocial;
                return $"{Nombre} {Apellido}";
            }
        }
        public string RUC { get; set; }
        public decimal Saldo { get; set; }
    }
}
