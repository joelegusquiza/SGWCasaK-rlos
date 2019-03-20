using AutoMapper;
using Core.DTOs.Cajas;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class CajasProfile : Profile
    {
        public CajasProfile()
        {
            CreateMap<Caja, CajaViewModel>()
                .ReverseMap();

            CreateMap<Caja, CajasAddViewModel>()
                .ReverseMap();

            CreateMap<CajasEditViewModel, Caja>()
                .ReverseMap();
        }
    }
}
