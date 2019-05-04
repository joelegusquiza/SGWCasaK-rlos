using Core.DTOs.CajaAperturaCierre;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ICajaAperturaCierre
    {
        List<CajaAperturaCierre> GetAll();
        CajaAperturaCierre GetById(int id);
        CajaAperturaCierre GetLastAperturaCierreByUser(int usuarioId);
        SystemValidationModel Save(AddCajaAperturaCierreViewModel viewModel);
    }
}
