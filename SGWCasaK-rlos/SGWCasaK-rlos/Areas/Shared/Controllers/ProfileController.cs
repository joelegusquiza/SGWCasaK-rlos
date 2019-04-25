using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Emails;
using Core.DTOs.Profile;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SGWCasaK_rlos.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class ProfileController : BaseController
    {
        private readonly IUsuarios _usuarios;
        private readonly IEmailSender _emailSender;
        private readonly IEnvironmentContext _environment;
        public ProfileController(IUsuarios usuarios, IEmailSender emailSender, IEnvironmentContext environment)
        {
            _usuarios = usuarios;
            _emailSender = emailSender;
            _environment = environment;
        }
        public IActionResult Index()
        {
            var viewModel = Mapper.Map<ProfileViewModel>(_usuarios.GetById(UserId));
            return View(viewModel);
        }
        public IActionResult UnverifiedEmail()
        {            
            return View();
        }

        [HttpPost]
        public async Task<SystemValidationModel> ResendEmail()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;


            var usuario = _usuarios.GetByEmail(email);
            usuario.UserVerifyEmailGuid = Guid.NewGuid();
            var result = _usuarios.Edit(usuario);
            if (result.Success)
            {
                var emailModel = new EmailModel()
                {
                    From = "noreply@casak-rlos.com.py",
                    FromName = "Casa K-rlos",
                    HtmlContent = $"Haga click <a href='{_environment.BaseUrl()}/Shared/Login/ConfirmEmail?userVerifyEmailGuid={usuario.UserVerifyEmailGuid.ToString()}'>aqui</a> para activar su cuenta.",
                    Subject = "Email de Activacion de Cuenta",
                    To = email,
                    ToName = $"{usuario.Nombre} {usuario.Apellido}"
                };
                await _emailSender.SendEmailAsync(emailModel);
                result.Message = "Se ha reenviado el email";
            }

            return result;
        }



        [HttpPost]
        public SystemValidationModel Modify(string model)
        {    
            var viewModel = JsonConvert.DeserializeObject<ProfileViewModel>(model);
            return _usuarios.Edit(viewModel);
        }
    }
}