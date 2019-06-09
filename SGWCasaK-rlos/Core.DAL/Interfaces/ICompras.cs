using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ICompras
    {
        List<Compra> GetAll();
        List<Compra> GetToPayByProveedorId(int proveedorId);
        Compra GetById(int id);
        List<Compra> GetAllPendings();
        List<Compra> GetAllWithProveedor();
        SystemValidationModel Save(ComprasAddViewModel viewModel);
        SystemValidationModel ConfirmCompra(int id, int sucursalId);
        SystemValidationModel Anular(int id, string razon);


    }
}
