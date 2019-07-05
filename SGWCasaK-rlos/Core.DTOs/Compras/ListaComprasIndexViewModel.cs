using Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DTOs.Compras
{
    public class ListaComprasIndexViewModel
    {
        public ListaComprasViewModel Compra { get; set; } = new ListaComprasViewModel();
        public List<ListaComprasViewModel> Compras { get; set; } = new List<ListaComprasViewModel>();
    }

    public class ListaComprasViewModel
    {
        public int CompraId { get; set; }
        public decimal Monto { get; set; }
        public int ProveedorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTimeOffset DateCompra { get; set; }
        public string RazonSocial { get; set; }
        public EstadoCompra Estado { get; set; }
        public string EstadoDescripcion => Estado.GetDescription();
        public string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(RazonSocial))
                    return RazonSocial;
                return $"{Nombre} {Apellido}";
            }
        }
    }
}
