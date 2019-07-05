using Core.DTOs.Shared;
using Core.DTOs.Sucursales;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ISucursales
    {
        IQueryable<Sucursal> GetAll();
        Sucursal GetById(int id);
        SystemValidationModel Save(SucursalesAddViewModel viewModel);
        SystemValidationModel Edit(SucursalesEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
