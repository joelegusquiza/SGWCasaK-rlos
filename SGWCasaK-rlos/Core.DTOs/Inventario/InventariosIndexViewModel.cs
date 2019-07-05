using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Inventario
{
    public class InventariosIndexViewModel
    {
        public List<InventarioViewModel> Inventarios { get; set; } = new List<InventarioViewModel>();
    }

    public class InventarioViewModel
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public InventarioEstado Estado { get; set; }
        public string EstadoDescription => Estado.GetDescription();

    }

    
}
