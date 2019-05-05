using Core.DTOs.Productos;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IProductos
    {
        IQueryable<Producto> GetAll();
        IQueryable<ProductoSucursal> GetProductoSucursal(List<int> productoIds, int sucursalId);
        List<ProductoViewModel> GetAllWithFormatedStock(int sucursalId);
        List<ProductoSucursalViewModel> GetAllBySucursales(List<int> sucursalesIds);
        Producto GetById(int id);
        SystemValidationModel ValidateStockPedido(List<DetallePedido> detallesPedio);
        SystemValidationModel Save(ProductosAddViewModel viewModel);
        SystemValidationModel Edit(ProductosEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        SystemValidationModel AddToSucursal(int id, int sucursalId);


    }
}
