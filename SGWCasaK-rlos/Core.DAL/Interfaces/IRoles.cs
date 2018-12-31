using Core.DTOs.Roles;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IRoles
    {
        List<Rol> GetAll();
        Rol GetById(int id);
        SystemValidationModel Save(RolesAddViewModel viewModel);
        SystemValidationModel Edit(RolesEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
