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
        List<Producto> GetAllWithPresentacion();
        Producto GetById(int id);
        SystemValidationModel ValidateStockPedido(List<DetallePedido> detallesPedio);
        SystemValidationModel Save(ProductosAddViewModel viewModel);
        SystemValidationModel Edit(ProductosEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        
    }
}
