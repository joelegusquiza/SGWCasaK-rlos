using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Clientes
{
    public class ClientesEditViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Ruc { get; set; }

        public ClientesDireccionesViewModel Direccion { get; set; } = new ClientesDireccionesViewModel();
        public List<ClientesDireccionesViewModel> Direcciones { get; set; } = new List<ClientesDireccionesViewModel>();
    }
}
