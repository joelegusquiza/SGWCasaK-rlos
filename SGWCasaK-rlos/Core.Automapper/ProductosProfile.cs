using AutoMapper;
using Core.DTOs.Productos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class ProductosProfile : Profile
    {
        public ProductosProfile()
        {
            CreateMap<Producto, ListaProductoViewModel>()
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StockActual, opt => opt.MapFrom(src => src.Stock))
                .ReverseMap();

            CreateMap<ProductoPresentacion, PresentacionProductoViewModel>()
                .ForMember(dest => dest.PresentacionId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Producto, ProductosAddViewModel>()                
                .ReverseMap();

            CreateMap<ProductosEditViewModel, Producto>()
                .ForMember(dest => dest.ProductoPresentaciones, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductoPresentacion, ProductoPresentacionesViewModel>()
                .ReverseMap();

        }
    }
}
