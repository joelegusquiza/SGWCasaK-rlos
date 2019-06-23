using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Timbrados;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area ("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class TimbradosController : Controller
    {
        private readonly ITimbrados _timbrados;
        private readonly ICajas _cajas;
        private readonly ISucursales _sucursales;
        public TimbradosController(ITimbrados timbrados, ICajas cajas, ISucursales sucursales)
        {
            _timbrados = timbrados;
            _cajas = cajas;
            _sucursales = sucursales;
        }
        [Authorize(Policy = "IndexTimbrado")]
        public IActionResult Index()
        {
            var viewModel = new TimbradosIndexViewModel()
            {
                Timbrados = Mapper.Map<List<TimbradoViewModel>>(_timbrados.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new TimbradosAddViewModel() 
            {
                Cajas = _cajas.GetAll().Select(x => new AdditionalData() { Value = x.Id, Text = x.Nombre, AdditionalInt = x.SucursalId }).ToList(),
                Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = $"{x.Nombre} - Codigo Establecimiento: {x.CodigoEstablecimiento}" }).ToList()
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<TimbradosEditViewModel>(_timbrados.GetById(id));
            viewModel.Cajas = _cajas.GetAll().Select(x => new AdditionalData() { Value = x.Id, Text = x.Nombre, AdditionalInt = x.SucursalId }).ToList();
            viewModel.Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = $"{x.Nombre} - Codigo Establecimiento: {x.CodigoEstablecimiento}" }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddTimbrado")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<TimbradosAddViewModel>(model);
            if (viewModel.FechaInicio >= viewModel.FechaFin)
                return new SystemValidationModel() { Message = "La fecha de inicio no puede ser mayor o igual a la fecha fin"};
            if (viewModel.NroInicio >= viewModel.NroFin)
                return new SystemValidationModel() { Message = "El nro de Inicio no puede ser mayor o igual al numero fin" };
            return _timbrados.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditTimbrado")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<TimbradosEditViewModel>(model);
            if (viewModel.FechaInicio >= viewModel.FechaFin)
                return new SystemValidationModel() { Message = "La fecha de inicio no puede ser mayor o igual a la fecha fin" };
            if (viewModel.NroInicio >= viewModel.NroFin)
                return new SystemValidationModel() { Message = "El nro de Inicio no puede ser mayor o igual al numero fin" };
            return _timbrados.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteTimbrado")]
        public SystemValidationModel Desactivate(int id)
        {
            
            return _timbrados.Desactivate(id);
        }
    }
}