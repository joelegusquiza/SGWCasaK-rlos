using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.Profile;
using Core.DTOs.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SGWCasaK_rlos.Areas.Shared.Controllers
{
    [Area("Shared"), Authorize]
    public class ProfileController : BaseController
    {
        private readonly IUsuarios _usuarios;
        public ProfileController(IUsuarios usuarios)
        {
            _usuarios = usuarios;
        }
        public IActionResult Index()
        {
            var viewModel = Mapper.Map<ProfileViewModel>(_usuarios.GetById(UserId));
            return View(viewModel);
        }

        [HttpPost]
        public SystemValidationModel Modify(string model)
        {    
            var viewModel = JsonConvert.DeserializeObject<ProfileViewModel>(model);
            return _usuarios.Edit(viewModel);
        }
    }
}