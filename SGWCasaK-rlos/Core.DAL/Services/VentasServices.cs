using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class VentasServices: IVentas
    {
        private readonly DataContext _context;

        public VentasServices(DataContext context)
        {
            _context = context;
        }

        public List<Venta> GetAll()
        {
            return _context.Set<Venta>().ToList();
        }
    }
}
