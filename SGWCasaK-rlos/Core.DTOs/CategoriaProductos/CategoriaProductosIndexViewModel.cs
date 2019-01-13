using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.CategoriaProductos
{
    public class CategoriaProductosIndexViewModel
    {
        public List<CategoriaProductoViewModel> Categorias { get; set; } = new List<CategoriaProductoViewModel>();
    }

    public class CategoriaProductoViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}