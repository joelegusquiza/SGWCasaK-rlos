using AutoMapper;
using Core.DTOs.Inventario;
using Core.DTOs.Productos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class InventarioProfile : Profile
    {
        public InventarioProfile()
        {
            CreateMap<Inventario, InventarioViewModel>().ReverseMap();

            CreateMap<Inventario, InventariosEditViewModel>()
                .ForMember(x => x.DetalleInventario, opt => opt.Ignore());

			CreateMap<DetalleInventario, InventarioDetalleViewModel>().ReverseMap();

			CreateMap<InventarioAddViewModel, Inventario>();

			CreateMap<InventariosEditViewModel, Inventario>()
                .ForMember(x => x.DetalleInventario, opt => opt.Ignore());

            CreateMap<Inventario, InventariosViewViewModel>().ReverseMap();

            CreateMap<ProductoSucursalViewModel, InventarioDetalleViewModel>()
                .ForMember(x => x.ProductoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.ProductoNombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(x => x.StockActual, opt => opt.MapFrom(src => src.Stock))
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
