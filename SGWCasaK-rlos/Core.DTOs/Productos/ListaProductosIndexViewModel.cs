﻿using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Productos
{
    public class ListaProductosIndexViewModel
    {
        public ListaProductoViewModel Producto { get; set; } = new ListaProductoViewModel();
        public List<ListaProductoViewModel> Productos { get; set; } = new List<ListaProductoViewModel>();
    }

    public class ListaProductoViewModel
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int StockActual { get; set; }
        public PorcIva PorcentajeIva { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Equivalencia { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal MontoTotal { get; set; }
        

        public PresentacionProductoViewModel Presentacion { get; set; } = new PresentacionProductoViewModel();
        public List<PresentacionProductoViewModel> ProductoPresentaciones { get; set; } = new List<PresentacionProductoViewModel>();
    }

    public class PresentacionProductoViewModel
    { 
        public int PresentacionId { get; set; }
        public string Nombre{ get; set; }
        public int Equivalencia { get; set; }
        public decimal PrecioVenta { get; set; }
    }
}
