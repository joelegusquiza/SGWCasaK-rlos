using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CajaAperturaCierre;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Core.Constants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class CajaAperturaCierreController : BaseController
    {
        public readonly ICajaAperturaCierre _cajaAperturaCierre;
        public readonly ICajas _cajas;
        public readonly IUsuarios _usuarios;
        public readonly IVentas _ventas;
        public readonly ICompras _compras;
        public CajaAperturaCierreController(ICajaAperturaCierre cajaAperturaCierre, ICajas cajas, IVentas ventas, ICompras compras, IUsuarios usuarios)
        {
            _cajaAperturaCierre = cajaAperturaCierre;
            _cajas = cajas;
            _ventas = ventas;
            _compras = compras;
            _usuarios = usuarios;
        }

        [Authorize(Policy = "IndexAperturaCierreCaja")]
        public IActionResult Index()
        {
            var viewModel = new CajaAperturaCierreIndexViewModel()
            {
                CajasAperturaCierre = Mapper.Map<List<CajaAperturaCierreViewModel>>(_cajaAperturaCierre.GetAll())
            };
            return View(viewModel);
        }


        public IActionResult CajaAperturaCierre(int id)
        {
            
            var aperturaCierre = _cajaAperturaCierre.GetLastAperturaCierreByUser(UserId);
            if (aperturaCierre == null || aperturaCierre.FechaCierre != null)
            {
                var viewModel = new AddCajaAperturaCierreViewModel()
                {
                    Tipo = Core.Constants.CajaTipoOperacion.Apertura,
                    Cajas = _cajas.GetAllBySucusalId(SucursalId).Select(x => new DropDownViewModel<int>() { Text = $"{x.Nombre}", Value = x.Id }).ToList(),
                    FechaApertura = DateTimeOffset.Now,
                    UsuarioId = UserId
                    
                };
                return View(viewModel);
            }
            else
            {
                var cajaAperturaCierre = _cajaAperturaCierre.GetById(id);
                var viewModel = new AddCajaAperturaCierreViewModel()
                {
                    Tipo = Core.Constants.CajaTipoOperacion.Cierre,                    
                    FechaCierre = DateTimeOffset.Now,
                    UsuarioId = UserId,
                    Cajas = _cajas.GetAllBySucusalId(SucursalId).Select(x => new DropDownViewModel<int>() { Text = $"{x.Nombre}", Value = x.Id }).ToList(),
                    CajaId = cajaAperturaCierre.CajaId,
                    Id = cajaAperturaCierre.Id,
                    Monto = _ventas.GetVentaByCajaId(cajaAperturaCierre.CajaId, DateTime.UtcNow).Sum(x => x.MontoTotal)

                };
                return View(viewModel);
            }
           
        }

        public List<DropDownViewModel<int>> GetLastAperturaCierre()
        {
            var listToReturn = new List<DropDownViewModel<int>>();
            var item = new DropDownViewModel<int>();
            var aperturaCierre = _cajaAperturaCierre.GetLastAperturaCierreByUser(UserId);
            if (aperturaCierre == null || aperturaCierre.FechaCierre != null)
            {
                item.Text = $"Abrir Caja";
                item.Value = 0;
                
            }
            else
            {
                item.Text = $"Cerrar Caja {aperturaCierre.Caja.Nombre}";
                item.Value = aperturaCierre.Id;

            }
            listToReturn.Add(item);
            return listToReturn;
        }

        [HttpPost]       
        public async Task<SystemValidationModel> Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<AddCajaAperturaCierreViewModel>(model);
            var result = _cajaAperturaCierre.Save(viewModel);
            if (result.Success && viewModel.Tipo == CajaTipoOperacion.Apertura)
            {
                var usuario = _usuarios.GetForLogin(Email);
                var caja = _cajas.GetById(viewModel.CajaId);
                var claims = new ClaimsIdentity(SecurityHelper.GetUserClaims(usuario, usuario.Sucursal, caja), "Cookie");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claims));
            }
                
            return result;
        }

    }
}
