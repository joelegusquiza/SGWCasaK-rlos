using AutoMapper;
using Core.DTOs.Clientes;
using Core.DTOs.Login;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class ClientesProfile : Profile
    {
        public ClientesProfile()
        {
            CreateMap<Cliente, ListaClienteViewModel>()
                .ForMember(dest => dest.ClienteId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Cliente, RegisterViewModel>()              
               .ReverseMap();

        }
    }
}
