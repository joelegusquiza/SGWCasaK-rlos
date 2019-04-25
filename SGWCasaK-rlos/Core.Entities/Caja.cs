﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Caja : BaseEntity
    {
        public string Nombre { get; set; }

        public ICollection<Usuario> Usuarios { get; set; } = new HashSet<Usuario>();
        public ICollection<Timbrado> Timbrados { get; set; } = new HashSet<Timbrado>();
    }
}