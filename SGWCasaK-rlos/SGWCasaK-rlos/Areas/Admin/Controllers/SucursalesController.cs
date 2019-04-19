using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Sucursales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class SucursalesController : Controller
    {
        private readonly ISucursales _sucursales;
        public SucursalesController(ISucursales sucursales)
        {
            _sucursales = sucursales;
        }
        [Authorize(Policy = "IndexSucursal")]
        public IActionResult Index()
        {
            var viewModel = new SucursalesIndexViewModel() { Sucursales = Mapper.Map<List<SucursalViewModel>>( _sucursales.GetAll()) };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new SucursalesAddViewModel() { };

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {

            var viewModel = Mapper.Map<SucursalesEditViewModel>(_sucursales.GetById(id));
            return View(viewModel);
        }

        [HttpPost]
        public List<DropDownViewModel<int>> GetSucursales()
        {
            var sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id}).ToList();
            return sucursales;
        }

        [HttpPost]
        [Authorize(Policy = "AddSucursal")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<SucursalesAddViewModel>(model);
            return _sucursales.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditSucursal")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<SucursalesEditViewModel>(model);
            return _sucursales.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteSucursal")]
        public SystemValidationModel Desactivate(int id)
        {
            return _sucursales.Desactivate(id);
        }
    }
}
