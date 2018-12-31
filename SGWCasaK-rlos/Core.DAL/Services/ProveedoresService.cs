using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ProveedoresService : IProveedores
    {
        private readonly DataContext _context;

        public ProveedoresService(DataContext context)
        {
            _context = context;
        }

        public List<Proveedor> GetAll()
        {
            return _context.Set<Proveedor>().Where(x => x.Active).ToList();
        }
    }
}
