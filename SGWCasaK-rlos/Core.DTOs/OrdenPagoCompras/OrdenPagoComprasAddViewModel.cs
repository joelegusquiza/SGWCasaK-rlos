﻿using Core.DTOs.Compras;
using Core.DTOs.Proveedores;
using Core.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.OrdenPagoCompras
{
    public class OrdenPagoComprasAddViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public decimal MontoTotal { get; set; }
		public OrdenPagoCompraEstado Estado { get; set; }
		public ListaProveedorViewModel Proveedor { get; set; } = new ListaProveedorViewModel();
        public OrdenPagoComprasDetalleViewModel Compra { get; set; } = new OrdenPagoComprasDetalleViewModel();
		public AddPagoCompraViewModel PagoCompra { get; set; } = new AddPagoCompraViewModel();
		public List<OrdenPagoComprasDetalleViewModel> OrdenPagoDetalle { get; set; } = new List<OrdenPagoComprasDetalleViewModel>();
    }

    public class OrdenPagoComprasDetalleViewModel
    {
        public decimal Monto { get; set; }
        public DateTimeOffset DateCompra { get; set; }
        public int CompraId { get; set; }      
    }
}
