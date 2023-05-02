using System;
using System.Collections.Generic;

namespace ahorra_marcket.Modelos
{
    public partial class ProductoTiendum
    {
        public int IdProductoTienda { get; set; }
        public int IdTienda { get; set; }
        public int IdProducto { get; set; }
        public double Precio { get; set; }
        public DateTime FechaIng { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Tiendum IdTiendaNavigation { get; set; } = null!;
    }
}
