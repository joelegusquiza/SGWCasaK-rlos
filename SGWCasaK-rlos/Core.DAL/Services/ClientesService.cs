using ApplicationContext;
using Core.DAL.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DAL.Services
{
    public class ClientesService : IClientes
    {
        private readonly DataContext _context;

        public ClientesService(DataContext context)
        {
            _context = context;
        }

        public List<Cliente> GetAll()
        {
            return _context.Set<Cliente>().Where(x => x.Active).ToList();
        }
    }
}
