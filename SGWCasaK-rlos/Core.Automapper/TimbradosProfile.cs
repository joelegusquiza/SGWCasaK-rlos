using AutoMapper;
using Core.DTOs.Timbrados;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class TimbradosProfile : Profile
    {
        public TimbradosProfile()
        {
            CreateMap<Timbrado, TimbradoViewModel>()
                .ReverseMap();

            CreateMap<Timbrado, TimbradosAddViewModel>()
            .ReverseMap();

            CreateMap<Timbrado, TimbradosEditViewModel>()
           .ReverseMap();
        }
    }
}
