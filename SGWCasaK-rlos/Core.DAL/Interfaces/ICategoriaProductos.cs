using Core.DTOs.CategoriaProductos;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ICategoriaProductos
    {
        List<CategoriaProducto> GetAll();
        CategoriaProducto GetById(int id);
        SystemValidationModel Save(CategoriaProductosAddViewModel viewModel);
        SystemValidationModel Edit(CategoriaProductosEditViewModel viewModel);
    }
}
