using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Productos
{
    public class ProductosEditViewModel : ProductosAddViewModel
    {
        public int Id { get; set; }

        public string StockString { get; set; }
    }
}
