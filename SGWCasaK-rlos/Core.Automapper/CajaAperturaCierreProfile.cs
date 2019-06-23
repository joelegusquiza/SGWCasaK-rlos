using AutoMapper;
using Core.DTOs.CajaAperturaCierre;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class CajaAperturaCierreProfile : Profile
    {
        public CajaAperturaCierreProfile()
        {
            CreateMap<CajaAperturaCierre, AddCajaAperturaViewModel>()
                .ReverseMap();

            CreateMap<CajaAperturaCierre, CajaAperturaCierreViewModel>()
                 .ForMember(dest => dest.Caja, opt => opt.MapFrom(src => src.Caja.Nombre))
                 .ReverseMap();

			CreateMap<DetalleCajaAperturaCierre, CajaCierreDetalleViewModel>()
				 .ReverseMap();

			CreateMap<DetalleCajaAperturaCierre, CajaCierreDetalleViewModel>()
				.ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.DateCreated))
				.ReverseMap();

		}
	}
}
