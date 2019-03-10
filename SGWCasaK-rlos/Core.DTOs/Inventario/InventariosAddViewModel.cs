using Core.DTOs.Productos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Inventario
{
    public class InventariosAddViewModel
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public ListaProductoViewModel Producto { get; set; } = new ListaProductoViewModel();
        public InventarioDetalleViewModel ProductoInventario { get; set; } = new InventarioDetalleViewModel();
        public List<InventarioDetalleViewModel> DetalleInventario { get; set; } = new List<InventarioDetalleViewModel>();       
    }

    public class InventarioDetalleViewModel
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int StockAnterior { get; set; }
        public int StockActual { get; set; }
        public int Equivalencia { get; set; }

    }




}
