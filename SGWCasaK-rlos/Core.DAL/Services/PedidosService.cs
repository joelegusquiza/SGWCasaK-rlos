﻿using ApplicationContext;
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

		public List<Pedido> GetBySucursalId(int sucursalId)
		{
			return _context.Set<Pedido>().Where(x => x.Active && x.SucursalId == sucursalId).Include(x => x.Cliente).ToList();
		}
		public Pedido GetById(int id)
        {
            return _context.Set<Pedido>().Include(x => x.DetallePedido).Include(x => x.Cliente).FirstOrDefault(x => x.Active && x.Id == id);
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

        public SystemValidationModel ChangeEstado(int id, EstadoPedido estado)
        {
            var pedido = GetById(id);
            pedido.Estado = estado;
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
