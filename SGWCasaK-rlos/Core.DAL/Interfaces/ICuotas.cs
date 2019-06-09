using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DAL.Interfaces
{
	public interface ICuotas
	{
		List<Cuota> GetAll();
		List<Cuota> GetAllByClienteId(int clienteId, EstadoCuota estado);
		List<Cuota> GetAllByVentaId(int ventaId, EstadoCuota estado);
		Cuota GetById(int id);
	}
}
