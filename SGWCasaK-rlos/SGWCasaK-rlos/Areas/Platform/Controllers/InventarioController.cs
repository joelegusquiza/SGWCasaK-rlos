using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Inventario;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class InventarioController : Controller
    {
        private readonly IInventario _inventario;
        private readonly IUsuarios _usuarios;
        public InventarioController(IInventario inventario, IUsuarios usuarios)
        {
            _inventario = inventario;
            _usuarios = usuarios;
        }

        [Authorize(Policy = "IndexInventario")]
        public IActionResult Index()
        {
            var viewModel = new InventariosIndexViewModel()
            {
                Inventarios = Mapper.Map<List<InventarioViewModel>>(_inventario.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new InventariosAddViewModel();           
            return View(viewModel);
        }

        [Authorize(Policy = "ViewInventario")]
        public IActionResult View(int id)
        {
            var inventario = _inventario.GetInventarioForView(id);
            var viewModel = Mapper.Map<InventariosViewViewModel>(inventario);
            var user = _usuarios.GetById(inventario.UserCreatedId);
            viewModel.UserName = $"{user.Nombre} {user.Apellido}";
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddInventario")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<InventariosAddViewModel>(model);
            return _inventario.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteInventario")]
        public SystemValidationModel Desactivate(int id)
        {
          
            return _inventario.Desactivate(id);
        }
    }
}