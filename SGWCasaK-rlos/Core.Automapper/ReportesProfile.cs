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
			.ForMember(dest => dest.Timbrado, opt => opt.MapFrom(src => src.Timbrado.NroTimbrado))
				   .ReverseMap();

			CreateMap<Compra, ReportesComprasDetalleViewModel>()
			.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Proveedor.Nombre))
			.ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Proveedor.Apellido))
			.ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.Proveedor.RazonSocial))
				.ForMember(dest => dest.Timbrado, opt => opt.MapFrom(src => src.Timbrado))
				   .ReverseMap();

			CreateMap<Cliente, ReportesComprasDetalleViewModel>()
				   .ReverseMap();

			CreateMap<Pedido, ReportesPedidosDetalleViewModel>()
			.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
			.ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
			.ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.Cliente.RazonSocial))
		
			   .ReverseMap();
		}
	}
}
