using AutoMapper;
using Core.DTOs.CategoriaProductos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class CategoriaProductosProfile : Profile
    {
        public CategoriaProductosProfile()
        {
            CreateMap<CategoriaProducto, CategoriaProductoViewModel>()                
                .ReverseMap();

            CreateMap<CategoriaProducto, CategoriaProductosAddViewModel>()
                .ReverseMap();

            CreateMap<CategoriaProducto, CategoriaProductosEditViewModel>()
                .ReverseMap();

        }
    }
}
