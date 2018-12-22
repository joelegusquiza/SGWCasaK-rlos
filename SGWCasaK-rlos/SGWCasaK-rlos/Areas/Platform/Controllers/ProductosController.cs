using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SGWCasaK_rlos.Areas.Platform.Controllers
{
    [Area("Platform")]
    public class ProductosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListaProductos()
        {
            return View("~/Areas/Platform/Views/Productos/Shared/ListaProductos.cshtml");
        }
    }
}