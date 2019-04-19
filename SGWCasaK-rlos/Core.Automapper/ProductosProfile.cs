using AutoMapper;
using Core.DTOs.Productos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Automapper
{
    public class ProductosProfile : Profile
    {
        public ProductosProfile()
        {
            CreateMap<Producto, ProductoViewModel>()
                   .ForMember(dest => dest.StockString, opt => opt.MapFrom(src => Helpers.Helpers.FormatStock(src.Stock, src.ProductoPresentaciones.ToDictionary(x => x.Nombre, x => x.Equivalencia))))
                   .ReverseMap();
            CreateMap<Producto, ListaProductoViewModel>()
                .ForMember(dest => dest.ProductoId, opt => opt.MapFrom(src => src.Id))  
                .ForMember(dest => dest.StockString, opt => opt.MapFrom(src => Helpers.Helpers.FormatStock(src.Stock, src.ProductoPresentaciones.ToDictionary(x => x.Nombre, x => x.Equivalencia))))
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
