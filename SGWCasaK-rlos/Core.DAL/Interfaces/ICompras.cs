using Core.DTOs.Compras;
using Core.DTOs.Shared;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface ICompras
    {
        List<Compra> GetAll();
        List<Compra> GetAllWithProveedor();
        SystemValidationModel Save(ComprasAddViewModel viewModel);
        
    }
}
