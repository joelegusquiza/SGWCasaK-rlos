using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Roles
{
    public class RolesAddViewModel
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool IsCliente { get; set; }
        public List<PermisoViewModel> PermisosList { get; set; } = new List<PermisoViewModel>();
    }

    public class PermisoViewModel 
    {
        public AccessFunctions Permiso { get; set; }
        public string Nombre { get; set; }
        public bool Selected { get; set; }
    }
}
