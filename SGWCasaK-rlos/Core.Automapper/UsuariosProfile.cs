using AutoMapper;
using Core.DTOs.Login;
using Core.DTOs.Profile;
using Core.DTOs.Usuarios;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class UsuariosProfile : Profile
    {
        public UsuariosProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>()
                .ReverseMap();

            CreateMap<Usuario, UsuariosAddViewModel>()
                .ReverseMap();

            CreateMap<Usuario, UsuariosEditViewModel>()
                .ReverseMap();

            CreateMap<Usuario, RegisterViewModel>()
                .ReverseMap();

            CreateMap<Usuario, ProfileViewModel>()
                .ReverseMap();
        }
    }
}
