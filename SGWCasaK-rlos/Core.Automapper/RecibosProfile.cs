using AutoMapper;
using Core.DTOs.Recibos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
	public class RecibosProfile : Profile
	{
		public RecibosProfile()
		{

			CreateMap<Recibo, ReciboViewModel>()
				.ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Cliente.Nombre))
				.ForMember(dest => dest.Apellido, opt => opt.MapFrom(src => src.Cliente.Apellido))
				.ForMember(dest => dest.RazonSocial, opt => opt.MapFrom(src => src.Cliente.RazonSocial))
				.ReverseMap();

			CreateMap<RecibosAddViewModel, Recibo>()
				.ForMember(dest => dest.Cuotas, opt => opt.Ignore())
				.ReverseMap();

			CreateMap<Recibo, RecibosEditViewModel>()				
				.ReverseMap();
					
			CreateMap<Cuota, RecibosItemViewModel>()
				.ForMember(dest => dest.CuotaId, opt => opt.MapFrom(src => src.Id))
				.ReverseMap();
		}
	}
}
