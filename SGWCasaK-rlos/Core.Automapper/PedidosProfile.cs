using AutoMapper;
using Core.DTOs.Pedidos;
using Core.DTOs.PedidosCliente;
using Core.DTOs.Ventas;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class PedidosProfile : Profile
    {
        public PedidosProfile()
        {
            CreateMap<Pedido, PedidoViewModel>()
                .ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.Cliente.RazonSocial))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
                .ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
                .ReverseMap();           

            CreateMap<Pedido, PedidosAddViewModel>().ReverseMap();

            CreateMap<PedidosDetalleViewModel, DetallePedido>()
             .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Nombre))
            .ReverseMap();

            CreateMap<PedidosEditViewModel, Pedido>()
                .ForMember(dest => dest.DetallePedido, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PedidosDetalleViewModel, DetallePedido>()
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Nombre))
               .ReverseMap();

            CreateMap<DetallePedido, VentasDetalleAddViewModel>()
                .ReverseMap();

			CreateMap<DetalleVenta,DetallePedido >()
			.ForMember(dest => dest.Id, opt => opt.Ignore())
				.ReverseMap();

			#region Clientes

			CreateMap<Pedido, PedidoClienteViewModel>()              
               .ReverseMap();

            CreateMap<Pedido, PedidosClienteAddViewModel>()
              .ReverseMap();

            CreateMap<DetallePedido, PedidosClienteDetalleViewModel>()
             .ReverseMap();

            CreateMap<PedidosClienteEditViewModel, Pedido>()
              .ForMember(dest => dest.Id, opt => opt.Ignore())
              .ForMember(dest => dest.ClienteId, opt => opt.Ignore())
              .ForMember(dest => dest.DetallePedido, opt => opt.Ignore())
              .ReverseMap();


            #endregion
        }
    }
}
