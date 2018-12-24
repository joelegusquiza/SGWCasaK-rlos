using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ProductosServices : IProductos
    {
        private readonly DataContext _context;

        public ProductosServices(DataContext context)
        {
            _context = context;
        }

        public List<Producto> GetAll()
        {
            return _context.Set<Producto>().ToList();
        }
    }
}
