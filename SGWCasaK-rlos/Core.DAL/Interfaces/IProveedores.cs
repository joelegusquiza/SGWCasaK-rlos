using Core.DTOs.Proveedores;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IProveedores
    {
        List<Proveedor> GetAll();
        Proveedor GetById(int id);
        SystemValidationModel Upsert(ProveedoresUpserViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
