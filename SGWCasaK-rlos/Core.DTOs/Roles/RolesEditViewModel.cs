using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Roles
{
    public class RolesEditViewModel : RolesAddViewModel
    {
        public int Id { get; set; }
        public string Permisos { get; set; }        
    }   
}
