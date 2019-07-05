using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Clientes
{
    public class ListaClientesIndexViewModel
    {
        public List<ListaClienteViewModel> Clientes { get; set; } = new List<ListaClienteViewModel>();
    }

    public class ListaClienteViewModel
    {
        public int ClienteId { get; set; }
        public string DisplayName { 
            get 
            {
                if (!string.IsNullOrEmpty(RazonSocial))
                    return RazonSocial;
                return $"{Nombre} {Apellido}";
            } 
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }
    }
}
