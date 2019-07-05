using AutoMapper;
using Core.DTOs.Reportes;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
	public class ReportesProfile : Profile
	{
		public ReportesProfile()
		{
			CreateMap<Venta, ReportesVentasDetalleViewModel>()
				   .ReverseMap();

			CreateMap<Compra, ReportesComprasDetalleViewModel>()
				   .ReverseMap();

			CreateMap<Pedido, ReportesPedidosDetalleViewModel>()
			   .ReverseMap();
		}
	}
}
