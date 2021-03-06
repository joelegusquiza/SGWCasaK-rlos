﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace SGWCasaK_rlos.Areas.Shared.Controllers
{
    public class BaseController : Controller
    {
        private int _userId { get; set; }
        public int UserId
        {
            get
            {
                return _userId > 0 ? _userId : (_userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.UserId).Value));
            }
        }

        private int _clientId { get; set; }
        public int ClientId 
        {
            get
            {
                return _clientId > 0 ? _clientId : _clientId = GetClientId();
            }
        }

        private int _sucursalId { get; set; }
        public int SucursalId
        {
            get
            {
                return _sucursalId > 0 ? _sucursalId : (_sucursalId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.SucursalId)?.Value));
            }
        }

        private int _cajaId { get; set; }
        public int CajaId
        {
            get
            {
                return _cajaId > 0 ? _cajaId : _cajaId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.CajaId).Value);
            }
        }

		private int _cajaAperturaCierreId { get; set; }
		public int CajaAperturaCierreId
		{
			get
			{
				return _cajaAperturaCierreId > 0 ? _cajaAperturaCierreId : _cajaAperturaCierreId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.CajaAperturaCierreId).Value);
			}
		}

		private string _email { get; set; }
        public string Email
        {
            get
            {
                return !string.IsNullOrEmpty(_email) ? _email : (_email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value);
            }
        }

		private int _rolId { get; set; }
		public int RolId
		{
			get
			{
				return _cajaId > 0 ? _cajaId : (_cajaId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == CustomClaims.RolId).Value));
			}
		}

		private int GetClientId()
        {
            var clientIdString = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.ClientId)?.Value;
            if (!string.IsNullOrEmpty(clientIdString))
                return Convert.ToInt32(clientIdString);
            return 0;
        }

		private List<string> _permisos { get; set; } = new List<string>();
		public List<string> Permisos  
		{
			get
			{
				return _permisos.Count() > 0 ? _permisos : (_permisos = User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Permisos).Value.Split(',').ToList());
			}
		}
		

	}
}