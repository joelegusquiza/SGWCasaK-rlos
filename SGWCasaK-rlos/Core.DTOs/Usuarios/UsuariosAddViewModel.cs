using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Usuarios
{
    public class UsuariosAddViewModel
    {
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public int SucursalId { get; set; }
        public List<AdditionalData> Roles { get; set; } = new List<AdditionalData>();
        public List<DropDownViewModel<int>> Sucursales { get; set; } = new List<DropDownViewModel<int>>();
    }
}
