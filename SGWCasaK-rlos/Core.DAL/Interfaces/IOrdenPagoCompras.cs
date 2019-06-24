using Core.DTOs.OrdenPagoCompras;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IOrdenPagoCompras
    {
        List<OrdenPagoCompra> GetAll();
        List<OrdenPagoCompra> GetAllWithProveedores();
        OrdenPagoCompra GetById(int id);
        SystemValidationModel Save(OrdenPagoComprasAddViewModel viewModel);
        SystemValidationModel Anular(int id);
    }
}
