using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CategoriaProductos;
using Core.DTOs.Shared;
using Core.Entities;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class CategoriaProductosService : ICategoriaProductos
    {
        private readonly DataContext _context;

        public CategoriaProductosService(DataContext context)
        {
            _context = context;
        }
     
        public List<CategoriaProducto> GetAll()
        {
            return _context.Set<CategoriaProducto>().Where(x => x.Active).ToList();
        }

        public CategoriaProducto GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public CategoriaProducto GetByName(string name)
        {
            return GetAll().Where(x => x.Nombre.ToLower().TryTrim() == name.ToLower().TryTrim()).FirstOrDefault();
        }

        public SystemValidationModel Save(CategoriaProductosAddViewModel viewModel)
        {
            if (GetByName(viewModel.Nombre) != null)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una categoria con el mismo nombre" };
            var categoriaProducto = Mapper.Map<CategoriaProducto>(viewModel);

            _context.Entry(categoriaProducto).State = EntityState.Added;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = categoriaProducto.Id,
                Message = success ? "Se ha guardado correctamente la categoria" : "No se pudo guardar la categoria",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Edit(CategoriaProductosEditViewModel viewModel)
        {
         
            var checkCatageria = GetByName(viewModel.Nombre);
            if (checkCatageria != null && checkCatageria.Id != viewModel.Id)
                return new SystemValidationModel() { Success = false, Message = "Ya existe una categoria con el mismo nombre" };
            var categoriaProducto = GetById(viewModel.Id);
            categoriaProducto = Mapper.Map(viewModel, categoriaProducto);

            _context.Entry(categoriaProducto).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = categoriaProducto.Id,
                Message = success ? "Se ha modificado la categoria" : "No se pudo modificado la categoria",
                Success = success
            };
            return validation;
        }

        public SystemValidationModel Desactivate (int id)
        {
            var categoriaProducto = GetById(id);
            categoriaProducto.Active = false;
            _context.Entry(categoriaProducto).State = EntityState.Modified;
            var success = _context.SaveChanges() > 0;
            var validation = new SystemValidationModel()
            {
                Id = categoriaProducto.Id,
                Message = success ? "Se ha eliminado la categoria" : "No se pudo eliminar la categoria",
                Success = success
            };
            return validation;
        }

    }
}
