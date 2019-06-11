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
		Venta GetById(int id);	
		List<Venta> GetVentaByCajaId(int cajaId, DateTime date, Constants.EstadoVenta estado);
		List<Venta> GetVentaConfirmadoByClienteId(int clienteId);
		SystemValidationModel Save(VentasAddViewModel viewModel);
		SystemValidationModel Edit(VentasViewViewModel viewModel);
		SystemValidationModel Edit(Venta venta);
		SystemValidationModel Confirm(VentasViewViewModel viewModel);
		VentasViewViewModel GetForView(int id);
		SystemValidationModel Anular(int id);
		int GetValidNroFactura(int sucursalId, int cajaId);
		string GetNroFacturaString(int sucursalId, int cajaId, int nroFactura);

	}
}
