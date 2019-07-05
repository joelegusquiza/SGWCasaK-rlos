using AutoMapper;
using Core.DTOs.Dashboard;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
	public class DashboardProfile : Profile
	{
		public DashboardProfile()
		{
			CreateMap<Pedido, DashboardPedidoViewModel>()
				   .ReverseMap();

		}
	}
}
