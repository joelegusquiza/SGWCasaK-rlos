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

            CreateMap<DetalleVenta, VentasDetalleAddViewModel>()
                .ReverseMap();

        }
    }
}
