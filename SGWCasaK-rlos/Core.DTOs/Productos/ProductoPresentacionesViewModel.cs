using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Productos
{
    public class ProductoPresentacionesViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Equivalencia { get; set; }
    }
}
