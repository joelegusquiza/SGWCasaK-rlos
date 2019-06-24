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

            CreateMap<ProveedoresUpserViewModel, Proveedor>()
                .ForMember(dest => dest.RUC, opt => opt.MapFrom(src => $"{src.RUC}-{src.DigitoVerificador}"));


            CreateMap<Proveedor, ProveedoresUpserViewModel>()
                .ForMember(dest => dest.RUC, opt => opt.MapFrom(src => GetRuc(src.RUC)))
                 .ForMember(dest => dest.DigitoVerificador, opt => opt.MapFrom(src => GetDigitoVerificador(src.RUC)));
        }

        private int GetRuc(string Ruc)
        {
            var ruc = Convert.ToInt32(Ruc.Split("-")[0]);
            return ruc;
        }

        private int GetDigitoVerificador(string Ruc)
        {
            var digitoVerificador = Convert.ToInt32(Ruc.Split("-")[1]);
            return digitoVerificador;
        }
    }
}
