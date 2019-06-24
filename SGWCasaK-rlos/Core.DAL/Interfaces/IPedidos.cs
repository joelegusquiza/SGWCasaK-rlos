using Core.DTOs.Pedidos;
using Core.DTOs.PedidosCliente;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Core.Constants;

namespace Core.DAL.Interfaces
{
    public interface IPedidos
    {
        List<Pedido> GetAll();
		List<Pedido> GetAllWithDelivery(int sucursaId);
		List<Pedido> GetBySucursalId(int sucursalId);
		List<Pedido> GetByClientId(int clienteId);
		PedidosPdfModel GetPdfModel(int id);	
		Pedido GetById(int id);
		List<EstadoPedido> GetEstadoAvailable(int id, bool anularPedidoPermiso);
		SystemValidationModel Save(PedidosAddViewModel viewModel);
        SystemValidationModel Save(PedidosClienteAddViewModel viewModel);
        SystemValidationModel Edit(PedidosEditViewModel viewModel);
        SystemValidationModel Edit(PedidosClienteEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        SystemValidationModel ChangeEstado(int id, Constants.EstadoPedido estado);
    }
}
