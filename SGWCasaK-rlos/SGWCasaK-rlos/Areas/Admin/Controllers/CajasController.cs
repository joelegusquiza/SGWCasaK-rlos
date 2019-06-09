using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Cajas;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class CajasController : Controller
    {
        private readonly ICajas _cajas;
        private readonly ISucursales _sucursales;
        public CajasController(ICajas cajas, ISucursales sucursales)
        {
            _cajas = cajas;
            _sucursales = sucursales;
        }
        [Authorize(Policy = "IndexRole")]
        public IActionResult Index()
        {
            var viewModel = new CajasIndexViewModel()
            {
                Cajas = Mapper.Map<List<CajaViewModel>>(_cajas.GetAllWithSucursal())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new CajasAddViewModel() { Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() {  Text = x.Nombre, Value = x.Id}).ToList()};
            
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {

            var viewModel = Mapper.Map<CajasEditViewModel>(_cajas.GetById(id));
            viewModel.Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddCaja")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CajasAddViewModel>(model);
            return _cajas.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditCaja")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<CajasEditViewModel>(model);
            return _cajas.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteCaja")]
        public SystemValidationModel Desactivate(int id)
        {
            return _cajas.Desactivate(id);
        }

        public List<DropDownViewModel<int>> GetCajas()
        {
            var cajas = _cajas.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList();
            return cajas;
        }
    }
}