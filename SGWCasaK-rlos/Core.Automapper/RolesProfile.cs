using AutoMapper;
using Core.DTOs.Roles;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class RolesProfile : Profile
    {
        public RolesProfile()
        {
            CreateMap<Rol, RolViewModel>()                
                .ReverseMap();

            CreateMap<Rol, RolesAddViewModel>()
                .ReverseMap();

            CreateMap<RolesEditViewModel, Rol>()
                .ForMember(dest => dest.Nombre, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
