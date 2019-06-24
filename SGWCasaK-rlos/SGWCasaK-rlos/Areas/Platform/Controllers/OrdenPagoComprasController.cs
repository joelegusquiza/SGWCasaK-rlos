using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.OrdenPagoCompras;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class OrdenPagoComprasController : BaseController
    {
        private readonly IOrdenPagoCompras _ordenPagoCompras;
        public OrdenPagoComprasController(IOrdenPagoCompras ordenPagoCompras)
        {
            _ordenPagoCompras = ordenPagoCompras;
        }

        [Authorize(Policy = "IndexOrdenPagoCompras")]
        public IActionResult Index()
        {
            var viewModel = new OrdenPagoComprasIndexViewModel() { OrdenPagosCompra = Mapper.Map<List<OrdenPagoCompraViewModel> >(_ordenPagoCompras.GetAllWithProveedores()) };
            return View(viewModel);
        }

       
        public IActionResult Add()
        {
            var viewModel = new OrdenPagoComprasAddViewModel() { SucursalId = SucursalId};
            return View(viewModel);
        }

        public IActionResult View(int id)
        {
            var viewModel = Mapper.Map<OrdenPagoComprasAddViewModel>(_ordenPagoCompras.GetById(id));
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddOrdenPagoCompras")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<OrdenPagoComprasAddViewModel>(model);
            return _ordenPagoCompras.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AnularOrdenPagoCompras")]
        public SystemValidationModel Anular(int id)
        {
           
            return _ordenPagoCompras.Anular(id);
        }
    }
}
