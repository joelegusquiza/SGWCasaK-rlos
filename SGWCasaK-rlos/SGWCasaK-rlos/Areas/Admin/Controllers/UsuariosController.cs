using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UsuariosController : Controller
    {
        private readonly IUsuarios _usuarios;
        private readonly IRoles _roles;
        static string IndexUsuario;
        public UsuariosController(IUsuarios usuarios, IRoles roles)
        {
            _usuarios = usuarios;
            _roles = roles;
        }
        [Authorize(Policy = "IndexUsuario")]
        public IActionResult Index()
        {
            var viewModel = new UsuariosIndexViewModel()
            {
                Usuarios = Mapper.Map<List<UsuarioViewModel>>(_usuarios.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new UsuariosAddViewModel() 
            { 
                Roles = _roles.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = x.Nombre}).ToList()
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<UsuariosEditViewModel>(_usuarios.GetById(id));
            viewModel.Roles = _roles.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = x.Nombre }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddUsuario")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<UsuariosAddViewModel>(model);
            return _usuarios.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditUsuario")]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<UsuariosEditViewModel>(model);
            return _usuarios.Edit(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "DeleteUsuario")]
        public SystemValidationModel Desactivate(int id)
        {
            return _usuarios.Desactivate(id);
        }
    }
}