using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Pedidos;
using Core.DTOs.PedidosCliente;
using Core.DTOs.Shared;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Core.Constants;

namespace Core.DAL.Services
{
    public class PedidosService : IPedidos
    {
        private readonly DataContext _context;

        public PedidosService(DataContext context)
        {
            _context = context;
        }

        public List<Pedido> GetAll()
        {
            return _context.Set<Pedido>().Where(x => x.Active).ToList();
        }

		public List<Pedido> GetAllWithDelivery(int sucursaId)
		{
			return _context.Set<Pedido>().Where(x => x.Active&& x.Delivery && x.SucursalId == sucursaId).OrderBy(x => x.FechaEntrega).ToList();
		}

		public List<Pedido> GetBySucursalId(int sucursalId)
		{
			return _context.Set<Pedido>().Where(x => x.Active && x.SucursalId == sucursalId).Include(x => x.Cliente).OrderByDescending(x => x.Id).ToList();
		}

		public Pedido GetById(int id)
        {
            return _context.Set<Pedido>().Include(x => x.DetallePedido).Include(x => x.Cliente).FirstOrDefault(x => x.Active && x.Id == id);
        }

		public PedidosPdfModel GetPdfModel(int id)
		{
			var pedido = Mapper.Map<PedidosPdfModel>(GetById(id));
			return pedido;
		}

		public List<EstadoPedido> GetEstadoAvailable(int id, bool anularPedidoPermiso)
		{
			var listToReturn = new List<EstadoPedido>();
			var pedido = GetById(id);
			if (pedido.Estado == EstadoPedido.Anulado || pedido.Estado == EstadoPedido.Finalizado || pedido.Estado == EstadoPedido.EntregadoPorDelivery)
				return listToReturn;

			if (pedido.Estado == EstadoPedido.Pendiente)
			{
				listToReturn.Add(EstadoPedido.Preparado);
				if (anularPedidoPermiso)
					listToReturn.Add(EstadoPedido.Anulado);

			}
			//if (pedido.Estado == EstadoPedido.Preparado)
			//{
			//	if (pedido.Delivery)
			//		listToReturn.Add(EstadoPedido.EntregadoPorDelivery);
			//	else
			//		listToReturn.Add(EstadoPedido.Finalizado);
			//	if (anularPedidoPermiso)
			//		listToReturn.Add(EstadoPedido.Anulado);

			//}

			return listToReturn;
		}

        public List<Pedido> GetByClientId (int clienteId)
        {
            return GetAll().Where(x => x.ClienteId == clienteId).ToList();
        }

        public SystemValidationModel Save(PedidosAddViewModel viewModel)
        {
            var pedido = Mapper.Map<Pedido>(viewModel);
            foreach (var detalle in pedido.DetallePedido)
            {
                _context.Entry(detalle).State = EntityState.Added;
            }
            _context.Entry(pedido).State = EntityState.Added;

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = pedido.Id,
                Message = success ? "Se ha guardado correctamente el pedido" : "No se pudo guardar el pedido",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Save(PedidosClienteAddViewModel viewModel)
        {
            var pedido = Mapper.Map<Pedido>(viewModel);
            foreach (var detalle in pedido.DetallePedido)
            {
                _context.Entry(detalle).State = EntityState.Added;
            }
            _context.Entry(pedido).State = EntityState.Added;

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = pedido.Id,
                Message = success ? "Se ha guardado correctamente el pedido" : "No se pudo guardar el pedido",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(PedidosEditViewModel viewModel)
        {
            var pedido = GetById(viewModel.Id);
            pedido = Mapper.Map(viewModel, pedido);
            _context.Entry(pedido).State = EntityState.Modified;

            var detalleIdToDelete = pedido.DetallePedido.Select(x => x.Id).Except(viewModel.DetallePedido.Where(x => x.Id > 0).Select(x => x.Id));

            foreach (var detalle in pedido.DetallePedido.Where(x => detalleIdToDelete.Contains(x.Id)))
            {
                _context.Entry(detalle).State = EntityState.Deleted;
            }

            foreach (var detalle in viewModel.DetallePedido.Where(x => x.Id == 0))
            {
                var detallePedido = Mapper.Map<DetallePedido>(detalle);
                _context.Entry(detallePedido).State = EntityState.Added;
                pedido.DetallePedido.Add(detallePedido);
            }

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Success = success,
                Message = success ? "Se ha editado correctamente el pedido" : "No se pudo editar el pedido"
            };
            return validation;
        }

        public SystemValidationModel Edit(PedidosClienteEditViewModel viewModel)
        {
            var pedido = GetById(viewModel.Id);

            if (pedido.Estado == EstadoPedido.Finalizado)
                return new SystemValidationModel() { Message = "No se puede modificar un pedido ya finalizado", Success = false };

            pedido = Mapper.Map(viewModel, pedido);
            _context.Entry(pedido).State = EntityState.Modified;

            var detalleIdToDelete = pedido.DetallePedido.Select(x => x.Id).Except(viewModel.DetallePedido.Where(x => x.Id > 0).Select(x => x.Id));

            foreach (var detalle in pedido.DetallePedido.Where(x => detalleIdToDelete.Contains(x.Id)))
            {
                _context.Entry(detalle).State = EntityState.Deleted;
            }

            foreach (var detalle in viewModel.DetallePedido.Where(x => x.Id == 0))
            {
                var detallePedido = Mapper.Map<DetallePedido>(detalle);
                _context.Entry(detallePedido).State = EntityState.Added;
                pedido.DetallePedido.Add(detallePedido);
            }

            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Success = success,
                Message = success ? "Se ha editado correctamente el pedido" : "No se pudo editar el pedido"
            };
            return validation;
        }

        public SystemValidationModel Desactivate(int id)
        {
            var pedido = GetById(id);
            pedido.Active = false;
            _context.Entry(pedido).State = EntityState.Modified;
            foreach (var detalle in pedido.DetallePedido)
            {
                detalle.Active = false;
                _context.Entry(detalle).State = EntityState.Modified;
            }
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = pedido.Id,
                Message = success ? "Se ha eliminado correctamente el pedido" : "No se pudo eliminar el pedido",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel ChangeEstado(int id, EstadoPedido estado, string razonAnulado)
        {
            var pedido = GetById(id);
            pedido.Estado = estado;
			if (estado == EstadoPedido.Anulado)
				pedido.RazonAnulado = razonAnulado;
            _context.Entry(pedido).State = EntityState.Modified;
			if (estado == EstadoPedido.EntregadoPorDelivery && !pedido.Delivery)
				return new SystemValidationModel() {Success = false, Message = "El pedido no cuenta con deliver" };
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = pedido.Id,
                Message = success ? "Se ha cambiado correctamente el estado del pedido" : "No se pudo modificar",
                Success = success
            };
            return validation;
        }
     
    }
}
