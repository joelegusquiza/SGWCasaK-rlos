using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DAL.Interfaces;
using Core.DTOs.CajaAperturaCierre;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SGWCasaK_rlos.Areas.Shared.Controllers;
using SGWCasaK_rlos.SecurityHelpers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform"), Authorize, ServiceFilter(typeof(UserEmailActiveFilter))]
    public class CajaAperturaCierreController : BaseController
    {
        public readonly ICajaAperturaCierre _cajaAperturaCierre;
        public readonly ICajas _cajas;
        public CajaAperturaCierreController(ICajaAperturaCierre cajaAperturaCierre, ICajas cajas)
        {
            _cajaAperturaCierre = cajaAperturaCierre;
            _cajas = cajas;
        }


        public IActionResult CajaAperturaCierre()
        {
            
            var aperturaCierre = _cajaAperturaCierre.GetLastAperturaCierreByUser(UserId);
            if (aperturaCierre == null || aperturaCierre.Tipo == Core.Constants.CajaTipoOperacion.Cierre)
            {
                var viewModel = new AddCajaAperturaCierreViewModel()
                {
                    Tipo = Core.Constants.CajaTipoOperacion.Apertura,
                    Cajas = _cajas.GetAll().Select(x => new DropDownViewModel<int>() { Text = x.Nombre, Value = x.Id }).ToList(),
                    FechaApertura = DateTimeOffset.Now
                    
                };
                return View(viewModel);
            }
            else
            {
                //    item.Text = $"Cerrar Caja {aperturaCierre.Caja.Nombre}";
                //    item.Value = aperturaCierre.Id;
                return View();
            }
           
        }

        public DropDownViewModel<int> GetLastAperturaCierre()
        {
            var item = new DropDownViewModel<int>();
            var aperturaCierre = _cajaAperturaCierre.GetLastAperturaCierreByUser(UserId);
            if (aperturaCierre == null || aperturaCierre.Tipo == Core.Constants.CajaTipoOperacion.Cierre)
            {
                item.Text = $"Abrir Caja";
                item.Value = 0;
                
            }
            else
            {
                item.Text = $"Cerrar Caja {aperturaCierre.Caja.Nombre}";
                item.Value = aperturaCierre.Id;

            }
                
            return item;
        }

        [HttpPost]
       
        public SystemValidationModel Save(string model)
        {
            var viewModel = JsonConvert.DeserializeObject<AddCajaAperturaCierreViewModel>(model);
            return _cajaAperturaCierre.Save(viewModel);
        }

    }
}
