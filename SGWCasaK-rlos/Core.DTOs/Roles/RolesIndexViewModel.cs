using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Roles
{
    public class RolesIndexViewModel
    {
        public List<RolViewModel> Roles { get; set; } = new List<RolViewModel>();
    }

    public class RolViewModel
    { 
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }       

    }
}
