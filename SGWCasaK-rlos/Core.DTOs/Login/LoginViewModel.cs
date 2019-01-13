using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs.Login
{
    public class LoginIndexViewModel 
    {
        public LoginViewModel LoginModel { get; set; } = new LoginViewModel();
        public RegisterViewModel RegisterModel { get; set; } = new RegisterViewModel();
        public ResetViewModel ResetModel { get; set; } = new ResetViewModel();
    }

    public class ResetViewModel
    {
        public string Email { get; set; }
    }

    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }

    }

    public class LoginViewModel
    {   
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Recordar { get; set; }
    }
}
