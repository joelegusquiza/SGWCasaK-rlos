using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Pedidos;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class PedidosController : BaseController
    {
        private readonly IPedidos _pedidos;
        private readonly IProductos _productos;
        public PedidosController(IPedidos pedidos, IProductos productos)
        {
            _pedidos = pedidos;
            _productos = productos;
        }
        [Authorize(Policy = "IndexPedido")]
        public IActionResult Index()
        {
            var viewModel = new PedidosIndexViewModel()
            {
                Pedidos = Mapper.Map<List<PedidoViewModel>>(_pedidos.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new PedidosAddViewModel();
            return View(viewModel);
        }        

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<PedidosEditViewModel>(_pedidos.GetById(id));
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "GenerateVentaPedido")]
        public SystemValidationModel ValidarPedido(int pedidoId)
        {

            var pedido = _pedidos.GetById(pedidoId);            
            if (pedido.Estado == EstadoPedido.Finalizado)
                return new SystemValidationModel() { Success = false, Message = "El pedido no es valido para generar la venta" };
            var resultValidateStock = _productos.ValidateStockPedido(pedido.DetallePedido.ToList());
            if (!resultValidateStock.Success)
            {
                resultValidateStock.Message = "No existe la cantidad suficiente de algunos productos";
                return resultValidateStock;
            }
            resultValidateStock.Message = Url.Action("GenerateVentaFromPedido", "Ventas", new { area = "platform", pedidoId});
            return resultValidateStock;
        }
       
        [HttpPost]
        [Authorize(Policy = "AddPedido")]
        public SystemValidationModel Save(string model)
        {

            var viewModel = JsonConvert.DeserializeObject<PedidosAddViewModel>(model);
            return _pedidos.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditPedido")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<PedidosEditViewModel>(model);
            return _pedidos.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ChangeEstadoPedido")]
        public SystemValidationModel CambiarEstado(int id, EstadoPedido estado)
        {
            
            return _pedidos.ChangeEstado(id, estado);
        }

        //[HttpPost]
        //[Authorize(Policy = "IndexPedido")]
        //public SystemValidationModel GenerarVenta(int id, EstadoPedido estado)
        //{

        //    return _pedidos.ChangeEstado(id, estado);
        //}

        [HttpPost]
        [Authorize(Policy = "DeletePedido")]
        public SystemValidationModel Desactivate(int id)
        {

            return _pedidos.Desactivate(id);
        }

    }
}