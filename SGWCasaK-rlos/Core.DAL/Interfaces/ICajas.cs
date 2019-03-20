using Core.DTOs.Cajas;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ICajas
    {
        IQueryable<Caja> GetAll();
        Caja GetById(int id);
        SystemValidationModel Save(CajasAddViewModel viewModel);
        SystemValidationModel Edit(CajasEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
