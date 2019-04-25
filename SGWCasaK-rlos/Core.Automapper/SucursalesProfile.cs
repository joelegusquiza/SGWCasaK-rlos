using AutoMapper;
using Core.DTOs.Sucursales;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class SucursalesProfile : Profile
    {
        public SucursalesProfile()
        {
            CreateMap<Sucursal, SucursalViewModel>()
                .ReverseMap();

            CreateMap<Sucursal, SucursalesAddViewModel>()
                .ReverseMap();

            CreateMap<SucursalesEditViewModel, Sucursal>()               
                .ReverseMap();
        }
    }
}
