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

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area ("Platform"), Authorize]
    public class TimbradosController : Controller
    {
        private readonly ITimbrados _timbrados;
        public TimbradosController(ITimbrados timbrados)
        {
            _timbrados = timbrados;
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
            
            return View(new TimbradosAddViewModel());
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<TimbradosEditViewModel>(_timbrados.GetById(id));
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