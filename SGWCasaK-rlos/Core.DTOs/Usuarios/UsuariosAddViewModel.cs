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
        public int CajaId { get; set; }
        public List<DropDownViewModel<int>> Roles { get; set; } = new List<DropDownViewModel<int>>();
        public List<DropDownViewModel<int>> Cajas { get; set; } = new List<DropDownViewModel<int>>();
    }
}
