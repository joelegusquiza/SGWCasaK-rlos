using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IVentas
    {
        List<Venta> GetAll();
        SystemValidationModel Save(VentasAddViewModel viewModel);
    }
}
