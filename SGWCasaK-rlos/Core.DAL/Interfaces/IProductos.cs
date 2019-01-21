using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IProductos
    {
        List<Producto> GetAll();
        Producto GetById(int id);
        SystemValidationModel Save(ProductosAddViewModel viewModel);
        SystemValidationModel Edit(ProductosEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
