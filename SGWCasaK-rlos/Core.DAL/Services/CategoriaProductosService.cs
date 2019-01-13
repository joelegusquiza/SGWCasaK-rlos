using ApplicationContext;
using AutoMapper;
using Core.DAL.Interfaces;
using Core.DTOs.CategoriaProductos;
using Core.DTOs.Shared;
using Core.Entities;
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

        public SystemValidationModel Save(CategoriaProductosAddViewModel viewModel)
        {
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

    }
}
