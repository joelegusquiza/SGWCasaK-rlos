using AutoMapper;
using Core.DTOs.Cuotas;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
	public class CuotasProfile : Profile
	{
		public CuotasProfile()
		{
			CreateMap<CuotaAddViewModel, Cuota>()
				.ReverseMap();

			CreateMap<Cuota, ListaCuotaViewModel >()
			.ForMember(dest=> dest.CuotaId, opt => opt.MapFrom(src => src.Id))
							.ReverseMap();
		}

	}
}