﻿using AutoMapper;
using Core.DTOs.Compras;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Automapper
{
    public class ComprasProfile : Profile
    {
        public ComprasProfile()
        {
            CreateMap<Compra, CompraViewModel>()
                .ForMember(dest => dest.Proveedor, opt => opt.MapFrom(src => GetProveedorNombre(src.Proveedor)))
                .ReverseMap();

            CreateMap<ComprasAddViewModel, Compra>()
                .ForMember(dest => dest.ProveedorId, opt => opt.MapFrom(src => GetProveedorId(src.Proveedor.ProveedorId)))
                .ReverseMap();

            CreateMap<ComprasDetalleAddViewModel, DetalleCompra>()
               .ReverseMap();
        }

        public int? GetProveedorId(int proveedorId)
        {
            if (proveedorId == 0)
                return null;
            return proveedorId;
        }

        public string GetProveedorNombre(Proveedor proveedor)
        {
            if (proveedor != null)
            {
                if (!string.IsNullOrEmpty(proveedor.RazonSocial))
                    return proveedor.RazonSocial;
                return $"{proveedor.Nombre} {proveedor.Apellido}";
            }
            return string.Empty;
        }
    }
}