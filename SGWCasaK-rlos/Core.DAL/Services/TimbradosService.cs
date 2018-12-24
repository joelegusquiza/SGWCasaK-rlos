using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class TimbradosService : ITimbrados
    {
        private readonly DataContext _context;

        public TimbradosService(DataContext context)
        {
            _context = context;
        }

        public Timbrado GetValidTimbrado ()
        {
            return _context.Set<Timbrado>().FirstOrDefault(x => DateTime.Now >= x.FechaInicio  && DateTime.Now <= x.FechaFin);
        }
    }
}
