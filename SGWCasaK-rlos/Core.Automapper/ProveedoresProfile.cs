using AutoMapper;
using Core.DTOs.Proveedores;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class ProveedoresProfile : Profile
    {
        public ProveedoresProfile()
        {
            CreateMap<Proveedor, ListaProveedorViewModel>()
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Proveedor, ProveedorViewModel>()               
               .ReverseMap();

            CreateMap<Proveedor, ProveedoresUpserViewModel>()
                .ReverseMap();
        }
    }
}
