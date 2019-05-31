using Core.DTOs.Pedidos;
using Core.DTOs.PedidosCliente;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IPedidos
    {
        List<Pedido> GetAll();
		List<Pedido> GetBySucursalId(int sucursalId);
		List<Pedido> GetByClientId(int clienteId);
        Pedido GetById(int id);
        SystemValidationModel Save(PedidosAddViewModel viewModel);
        SystemValidationModel Save(PedidosClienteAddViewModel viewModel);
        SystemValidationModel Edit(PedidosEditViewModel viewModel);
        SystemValidationModel Edit(PedidosClienteEditViewModel viewModel);
        SystemValidationModel Desactivate(int id);
        SystemValidationModel ChangeEstado(int id, Constants.EstadoPedido estado);
    }
}
