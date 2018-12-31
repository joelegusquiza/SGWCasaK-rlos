﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Roles;
using Core.DTOs.Shared;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IRoles _roles;
        public RolesController(IRoles roles)
        {
            _roles = roles;
        }

        public IActionResult Index()
        {
            var viewModel = new RolesIndexViewModel()
            {
                Roles = Mapper.Map<List<RolViewModel>>(_roles.GetAll())
            };
            return View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new RolesAddViewModel() { };
            viewModel.PermisosList = Enum.GetValues(typeof(AccessFunctions)).Cast<AccessFunctions>().Select(x => new PermisoViewModel() { 
                Permiso = x,
                Nombre = x.GetDescription()
            }).ToList();
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {

            var viewModel = Mapper.Map<RolesEditViewModel>(_roles.GetById(id));
            viewModel.PermisosList = Enum.GetValues(typeof(AccessFunctions)).Cast<AccessFunctions>().Select(x => new PermisoViewModel()
            {
                Permiso = x,
                Nombre = x.GetDescription()
            }).ToList();
            foreach (var p in viewModel.Permisos.Split(",").Select(x =>  ((AccessFunctions)Convert.ToInt32(x))))
            {
                var permiso = viewModel.PermisosList.FirstOrDefault(x => x.Permiso == p);
                permiso.Selected = true;
            }
            return View(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<RolesAddViewModel>(model);
            return _roles.Save(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<RolesEditViewModel>(model);
            return _roles.Edit(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Desactivate(int id)
        {
            return _roles.Desactivate(id);
        }
    }
}