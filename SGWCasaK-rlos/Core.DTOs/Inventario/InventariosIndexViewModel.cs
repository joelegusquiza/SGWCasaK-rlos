using System;
using System.Collections.Generic;
using System.Text;

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
        public int UserId { get; set; }
        public string User { get; set; }

    }

    
}
