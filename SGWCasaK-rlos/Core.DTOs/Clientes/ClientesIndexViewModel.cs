using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Clientes
{
    public class ClientesIndexViewModel
    {
        public List<ClienteViewModel> Clientes { get; set; } = new List<ClienteViewModel>();
    }

    public class ClienteViewModel
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
        public string Telefono { get; set; }
        public string Ruc { get; set; }
        public decimal Saldo { get; set; }
    }
}
