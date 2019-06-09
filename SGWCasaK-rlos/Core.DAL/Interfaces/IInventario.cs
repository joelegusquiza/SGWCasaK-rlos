using Core.DTOs.Inventario;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IInventario
    {
        IQueryable<Inventario> GetAll();
        Inventario GetById(int Id);
        Inventario GetInventarioForView(int Id);

		SystemValidationModel Save(InventarioAddViewModel viewModel);
		SystemValidationModel Edit(InventariosEditViewModel viewModel);
		SystemValidationModel Anular(int id, string razon);
    }
}
