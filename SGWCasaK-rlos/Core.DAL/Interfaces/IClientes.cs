using Core.DTOs.Clientes;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IClientes
    {
        List<Cliente> GetAll();
        Cliente GetById(int id);
        SystemValidationModel Save(ClientesAddViewModel viewModel);
        SystemValidationModel Edit(ClientesEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
    }
}
