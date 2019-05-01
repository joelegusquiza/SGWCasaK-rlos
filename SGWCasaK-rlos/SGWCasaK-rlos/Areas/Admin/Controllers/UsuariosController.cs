using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Emails;
using Core.DTOs.Shared;
using Core.DTOs.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;
using static Core.Constants;

namespace SGWCasaK_rlos.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize , ServiceFilter(typeof(UserEmailActiveFilter))]
    public class UsuariosController : Controller
    {
        private readonly IUsuarios _usuarios;
        private readonly IRoles _roles;
        private readonly ISucursales _sucursales;
        private readonly IEmailSender _emailSender;
        private readonly IEnvironmentContext _environment;

        public UsuariosController(IUsuarios usuarios, IRoles roles, IEmailSender emailSender, IEnvironmentContext environment, ISucursales sucursales)
        {
            _usuarios = usuarios;
            _roles = roles;
            _emailSender = emailSender;
            _environment = environment;
            _sucursales = sucursales;
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
                Roles = _roles.GetAll().Select(x => new AdditionalData() { Value = x.Id, Text = x.Nombre, AdditionalBool = x.IsCliente }).ToList(),
                Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = x.Nombre }).ToList(),
            };
            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var viewModel = Mapper.Map<UsuariosEditViewModel>(_usuarios.GetById(id));
            viewModel.Roles = _roles.GetAll().Select(x => new AdditionalData() { Value = x.Id, Text = x.Nombre, AdditionalBool = x.IsCliente }).ToList();
            viewModel.Sucursales = _sucursales.GetAll().Select(x => new DropDownViewModel<int>() { Value = x.Id, Text = x.Nombre }).ToList();
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "AddUsuario")]
        public async Task<SystemValidationModel> Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<UsuariosAddViewModel>(model);  
            var result = _usuarios.Save(viewModel);
            if (result.Success)
            {
                var usuario = _usuarios.GetById(result.Id);
                usuario.UserVerifyEmailGuid = Guid.NewGuid();
                var success = _usuarios.Edit(usuario);
                var emailModel = new EmailModel()
                {
                    From = "noreply@casak-rlos.com.py",
                    FromName = "Casa K-rlos",
                    HtmlContent = $"Haga click <a href='{_environment.BaseUrl()}/Shared/Login/ConfirmEmail?userVerifyEmailGuid={usuario.UserVerifyEmailGuid.ToString()}'>aqui</a> para activar su cuenta.",
                    Subject = "Email de Activacion de Cuenta",
                    To = viewModel.Email,
                    ToName = $"{usuario.Nombre} {usuario.Apellido}"
                };
                await _emailSender.SendEmailAsync(emailModel);
            }
            return result;
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