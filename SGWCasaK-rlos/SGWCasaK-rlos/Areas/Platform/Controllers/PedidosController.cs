using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Pedidos;
using Core.DTOs.Shared;
using Core.DTOs.Ventas;
using Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PdfServices.Interfaces;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class PedidosController : BaseController
    {
        private readonly IPedidos _pedidos;
		private readonly IPdfCreation _pdfCreation;
        private readonly IProductos _productos;
        public PedidosController(IPedidos pedidos, IProductos productos, IPdfCreation pdfCreation)
        {
            _pedidos = pedidos;
            _productos = productos;
			_pdfCreation = pdfCreation;
        }
        [Authorize(Policy = "IndexPedido")]
        public IActionResult Index()
        {
            var viewModel = new PedidosIndexViewModel()
            {
                Pedidos = Mapper.Map<List<PedidoViewModel>>(_pedidos.GetBySucursalId(SucursalId))
            };				
			return View(viewModel);
        }

		public IActionResult GetPedidoPdf(int id)
		{
			var model = _pedidos.GetPdfModel(id);
			byte[] byteArray = _pdfCreation.GetPedidosPdf(model);
			MemoryStream pdfStream = new MemoryStream();
			pdfStream.Write(byteArray, 0, byteArray.Length);
			pdfStream.Position = 0;
			return new FileStreamResult(pdfStream, "application/pdf");
			
		}

		[Authorize(Policy = "IndexPedido")]
		public IActionResult IndexDelivery()
		{
			var viewModel = new PedidosIndexViewModel()
			{
				Pedidos = Mapper.Map<List<PedidoViewModel>>(_pedidos.GetAllWithDelivery(SucursalId))
			};
			return View(viewModel);
		}

		[Authorize(Policy = "ChangeEstadoPedido")]
		public IActionResult ChangeEstadoPedidoIFrame(int id)
		{
			var viewModel = new PedidosChangeEstadoViewModel()
			{
				Id = id,
			};
			var estados = _pedidos.GetEstadoAvailable(id, Permisos.Contains(((int)AccessFunctions.AnularPedido).ToString()));
			viewModel.Estados = estados.Select(x => new DropDownViewModel<EstadoPedido>() { Text = x.GetDescription(), Value = x }).ToList();
			viewModel.Estado = estados.FirstOrDefault();
			return View(viewModel);
		}

		public IActionResult Add()
        {
            var viewModel = new PedidosAddViewModel() { SucursalId = SucursalId};
            return View(viewModel);
        }        

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<PedidosEditViewModel>(_pedidos.GetById(id));
			viewModel.SucursalId = SucursalId;
            return View(viewModel);
        }

		public IActionResult View(int id)
		{
			var viewModel = Mapper.Map<PedidosEditViewModel>(_pedidos.GetById(id));
			viewModel.SucursalId = SucursalId;
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
        public SystemValidationModel CambiarEstado(int id, EstadoPedido estado, string razonAnulado)
        {
            
            return _pedidos.ChangeEstado(id, estado, razonAnulado);
        }

        //[HttpPost]
        //[Authorize(Policy = "IndexPedido")]
        //public SystemValidationModel GenerarVenta(int id, EstadoPedido estado)
        //{

        //    return _pedidos.ChangeEstado(id, estado);
        //}

        //[HttpPost]
        //[Authorize(Policy = "AnularPedido")]
        //public SystemValidationModel Desactivate(int id)
        //{

        //    return _pedidos.ChangeEstado(id, EstadoPedido.Anulado);
        //}

    }
}