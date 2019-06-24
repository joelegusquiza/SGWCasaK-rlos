using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Usuarios
{
    public class UsuariosIndexViewModel
    {
        public List<UsuarioViewModel> Usuarios { get; set; } = new List<UsuarioViewModel>();
    }

    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
