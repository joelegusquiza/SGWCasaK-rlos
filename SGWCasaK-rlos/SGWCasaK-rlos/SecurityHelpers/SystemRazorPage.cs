using Core.Helpers;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Constants;

namespace SGWCasaK_rlos.SecurityHelpers
{
    public abstract class SystemRazorPage<T> : RazorPage<T>
    {
        public bool HasPermisos(AccessFunctions accessFunction)
        {
            return AccountPermisos.Contains(((int)accessFunction).ToString());
        }
        private List<string> _accountPermisos { get; set; } = null;
        public List<string> AccountPermisos
        {
            get
            {
                var permisos = _accountPermisos ?? (_accountPermisos = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Permisos).Value.Split(',').ToList());
                return permisos;
            }
        }
    }
}
