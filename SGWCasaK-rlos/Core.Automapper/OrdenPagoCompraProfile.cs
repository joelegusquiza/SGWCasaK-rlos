using AutoMapper;
using Core.DTOs.OrdenPagoCompras;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class OrdenPagoCompraProfile : Profile
    {
        public OrdenPagoCompraProfile()
        {
            CreateMap<OrdenPagoCompra, OrdenPagoCompraViewModel>()
                .ForMember(dest => dest.Proveedor, opt => opt.MapFrom( src => string.IsNullOrEmpty(src.Proveedor.Apellido) && string.IsNullOrEmpty(src.Proveedor.Nombre) ? $"{src.Proveedor.RazonSocial}" : $"{src.Proveedor.Apellido} {src.Proveedor.Nombre}"))
                .ReverseMap();

            CreateMap<OrdenPagoComprasAddViewModel, OrdenPagoCompra>()               
               .ReverseMap();

            CreateMap<OrdenPagoComprasDetalleViewModel, OrdenPagoCompraDetalle>()
               .ReverseMap();
        }

    }
}
