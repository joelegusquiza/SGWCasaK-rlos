using AutoMapper;
using Core.DTOs.Inventario;
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

            CreateMap<Inventario, InventariosAddViewModel>().ReverseMap();

            CreateMap<DetalleInventario, InventarioDetalleViewModel>().ForMember(x => x.ProductoNombre, opt => opt.MapFrom(src => src.Producto.Nombre)).ReverseMap();

            CreateMap<Inventario, InventariosViewViewModel>().ReverseMap();
        }
    }
}
