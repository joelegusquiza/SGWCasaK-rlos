﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DAL.Interfaces
{
    public interface IProductos
    {
        List<Producto> GetAll();
    }
}
