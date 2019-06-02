using AutoMapper;
using Core.DTOs.Ventas;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class VentasProfile : Profile
    {
        public VentasProfile()
        {
            CreateMap<Venta, VentaViewModel>()
                .ReverseMap();

            CreateMap<Venta, VentasAddViewModel>()
                .ReverseMap();

			CreateMap<Timbrado, VentasAddViewModel>()
			   .ReverseMap();

			CreateMap<VentasEditViewModel, Venta>()
				.ForMember(dest => dest.DetalleVenta, opt => opt.Ignore())
				.ForMember(dest => dest.Cliente, opt => opt.Ignore())
				.ReverseMap();

			CreateMap<VentasDetalleAddViewModel, DetalleVenta>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Nombre))
                .ReverseMap();

            CreateMap<Pedido, VentasAddViewModel>()
                .ForMember(dest => dest.DetalleVenta, opt => opt.MapFrom(src => src.DetallePedido))
                .ForMember(dest => dest.PedidoId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<DetallePedido, VentasDetalleAddViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Descripcion))
				.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ReverseMap();

			CreateMap<Venta, ListaVentasViewModel>()
			  .ForMember(dest => dest.VentaId, opt => opt.MapFrom(src => src.Id))
			  .ForMember(dest => dest.Monto, opt => opt.MapFrom(src => src.MontoTotal))
			  .ReverseMap();

		}
    }
}
