using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using Core.DTOs.Emails;
using Core.DTOs.Login;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.SecurityHelpers;

namespace SGWCasaK_rlos.Areas.Shared.Controllers
{
    [Area("Shared")]
    public class LoginController : Controller
    {
        private readonly IUsuarios _usuarios;
        private readonly IEmailSender _emailSender;
        private readonly IEnvironmentContext _environment;
        public LoginController(IUsuarios usuarios, IEnvironmentContext environment, IEmailSender emailSender)
        {
            _usuarios = usuarios;
            _emailSender = emailSender;
            _environment = environment;
        }

        public IActionResult Index()
        {
            var viewModel = new LoginIndexViewModel();
            return View(viewModel);
        }

        public IActionResult ChangePassword(string resetGuid)
        {
            var user = _usuarios.GetByGuid(resetGuid);
            if (user == null)
            {
                return RedirectToAction("Login", "User", new { area = "Shared" });
            }
            else
            {
                var viewModel = new ChangePasswordViewModel()
                {
                    Guid = resetGuid
                };
                return View(viewModel);
            }

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login", new { area = "Shared" });
        }

        public IActionResult ConfirmEmail(string userVerifyEmailGuid)
        {
            var user = _usuarios.GetBUserVerifyEmailGuid(userVerifyEmailGuid);
            user.EmailVerified = true;
            var success = _usuarios.Edit(user);
            return View(success.Success);

        }

        [HttpPost]
        public async Task<SystemValidationModel> Login(string model)
        {
            try
            {
                var viewModel = JsonConvert.DeserializeObject<LoginViewModel>(model);
                var usuario = _usuarios.GetForLogin(viewModel.Email);
                if (usuario == null)
                    return new SystemValidationModel() { Success = false, Message = "El usuario no existe" };
                var success = usuario.CheckPassword(viewModel.Password);
                if (success)
                {
                    ClaimsIdentity claims;
                    if (usuario.Cliente == null)
                        claims = new ClaimsIdentity(SecurityHelper.GetUserClaims(usuario), "Cookie");
                    else
                        claims = new ClaimsIdentity(SecurityHelper.GetUserClaims(usuario, usuario.Cliente), "Cookie");

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims));
                    return new SystemValidationModel() { Success = true, Message = "Login Exitoso", Url = Url.Action("Index", "Dashboard", new { area = "platform" }) };
                }
                return new SystemValidationModel() { Success = false, Message = "Password Incorrecto" };
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        
        [HttpPost]
        public async Task<SystemValidationModel> Register(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<RegisterViewModel>(model);
            var result = _usuarios.Register(viewModel);
            if (result.Success)
            {
                var usuario = _usuarios.GetById(result.Id);
                usuario.UserVerifyEmailGuid = Guid.NewGuid();
                var success = _usuarios.Edit(usuario);
                if (success.Success)
                {
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
            }
            return result;
        }

        [HttpPost]
        public async Task<SystemValidationModel> Reset(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ResetViewModel>(model);
            var usuario = _usuarios.GetByEmail(viewModel.Email);
            if (usuario == null)
                return new SystemValidationModel() { Success = false, Message = "No existe una cuenta registrada con ese email" };
            usuario.Guid = Guid.NewGuid();
            usuario.Expiration = DateTime.UtcNow.AddMinutes(30);
            var success = _usuarios.Edit(usuario);
            if (!success.Success)
                return new SystemValidationModel() { Success = false, Message = "Ocurrio un eror por favor intentelo de nuevo" };
            var emailModel = new EmailModel()
            {

                From = "noreply@casak-rlos.com.py",
                FromName = "Casa K-rlos",
                HtmlContent = $"Haga click <a href='{_environment.BaseUrl()}/Shared/Login/ChangePassword?resetGuid={usuario.Guid}'>aqui</a> para recuperar su cuenta.",
                Subject = "Email de Recuperacion de Contraseña",
                To = viewModel.Email,
                ToName = $"{usuario.Nombre} {usuario.Apellido}"
            };
            await _emailSender.SendEmailAsync(emailModel);
            return new SystemValidationModel() { Success = true, Message = "Se ha enviado un correo de recuperacion de password a su email" };
        }

        [HttpPost]
        public SystemValidationModel UpdatePassword(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<ChangePasswordViewModel>(model);
            return _usuarios.UpdatePassword(viewModel);
        }
    }
}