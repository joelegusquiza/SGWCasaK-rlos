using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Proveedores
{
    public class ListaProveedoresIndexViewModel
    {
        public List<ListaProveedorViewModel> Proveedores { get; set; } = new List<ListaProveedorViewModel>();
    }

    public class ListaProveedorViewModel
    {
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string DisplayName { 
            get {      
                if (string.IsNullOrEmpty(RazonSocial)) 
                {
                    return $"{Nombre} {Apellido}";
                }else{
                        return RazonSocial;
                }        
            } 
        }
        public string RUC { get; set; }
        public string Telefono { get; set; }
        public decimal Saldo { get; set; }
    }
}
