using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Roles;
using Core.DTOs.Shared;
using Core.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class RolesController : BaseController
    {
        private readonly IRoles _roles;
		private readonly IUsuarios _usuarios;
		private readonly ICajas _cajas;
		public RolesController(IRoles roles, IUsuarios usuarios, ICajas cajas)
        {
            _roles = roles;
			_usuarios = usuarios;
			_cajas = cajas;
        }
        [Authorize(Policy = "IndexRole")]
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
        [Authorize(Policy = "AddRole")]
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<RolesAddViewModel>(model);
            return _roles.Save(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "EditRole")]
        public async Task<SystemValidationModel> Edit(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<RolesEditViewModel>(model);
			
			var result = _roles.Edit(viewModel);
			if (result.Success && RolId == viewModel.Id)
			{
				var usuario = _usuarios.GetForLogin(Email);
				var claims = new ClaimsIdentity();
				if (CajaId != 0)
				{
					var caja = _cajas.GetById(CajaId);
					claims = new ClaimsIdentity(SecurityHelper.GetUserClaims(usuario, usuario.Sucursal, caja), "Cookie");
				}
				else
				{
					claims = new ClaimsIdentity(SecurityHelper.GetUserClaims(usuario, usuario.Sucursal), "Cookie");
				}

				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims));
			}
			return result;
        }

        [HttpPost]
        [Authorize(Policy = "DeleteRole")]
        public SystemValidationModel Desactivate(int id)
        {
            return _roles.Desactivate(id);
        }
    }
}