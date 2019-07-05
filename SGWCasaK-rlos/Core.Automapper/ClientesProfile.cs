using AutoMapper;
using Core.DTOs.Clientes;
using Core.DTOs.Login;
using Core.DTOs.Usuarios;
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

            CreateMap<Cliente, ClienteViewModel>()
               .ReverseMap();

            CreateMap<Cliente, ClientesAddViewModel>()
               .ReverseMap();

            CreateMap<ClientesEditViewModel, Cliente>()
               .ForMember(dest => dest.Direcciones, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<Direccion, ClientesDireccionesViewModel>()
               .ReverseMap();

            CreateMap<UsuariosAddViewModel, Cliente>()
            .ReverseMap();

        }
    }
}
