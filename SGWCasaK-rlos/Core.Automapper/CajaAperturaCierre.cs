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
            CreateMap<CajaAperturaCierre, AddCajaAperturaCierreViewModel>()
                .ReverseMap();

           
        }
    }
}
