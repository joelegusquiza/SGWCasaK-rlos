using Core.DTOs.Recibos;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
	public interface IRecibos
	{
		List<Recibo> GetAll();
		Recibo GetById(int id);
		Recibo GetByIdWithCliente(int id);
		List<Recibo> GetAllWithCliente();
		SystemValidationModel Save(RecibosAddViewModel viewModel);
		SystemValidationModel Confirmar(RecibosEditViewModel viewModel);
		SystemValidationModel Desactivate(int id);
	}
}
