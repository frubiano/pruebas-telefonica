using System;
using System.Collections.Generic;

namespace ahorra_marcket.Modelos
{
    public partial class Usuario
    {
        public Usuario()
        {
            UsuariosPerfiles = new HashSet<UsuariosPerfile>();
        }

        public int IdUser { get; set; }
        public string Nombre { get; set; } = null!;
        public string Pass { get; set; } = null!;
        public DateTime FechaIng { get; set; }

        public virtual ICollection<UsuariosPerfile> UsuariosPerfiles { get; set; }
    }
}
