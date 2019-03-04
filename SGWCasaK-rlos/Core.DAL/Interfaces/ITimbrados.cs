using Core.DTOs.Shared;
using Core.DTOs.Timbrados;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ITimbrados
    {
        List<Timbrado> GetAll();
        Timbrado GetById(int id);
        Timbrado GetValidTimbrado();
        SystemValidationModel Edit(TimbradosEditViewModel viewModel);
        SystemValidationModel Save(TimbradosAddViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
