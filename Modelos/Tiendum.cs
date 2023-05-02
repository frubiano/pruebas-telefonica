﻿using System;
using System.Collections.Generic;

namespace ahorra_marcket.Modelos
{
    public partial class Tiendum
    {
        public Tiendum()
        {
            ProductoTienda = new HashSet<ProductoTiendum>();
        }

        public int IdTienda { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaIng { get; set; }

        public virtual ICollection<ProductoTiendum> ProductoTienda { get; set; }
    }
}
