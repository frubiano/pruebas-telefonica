using System;
using System.Collections.Generic;

namespace ahorra_marcket.Modelos
{
    public partial class Perfile
    {
        public Perfile()
        {
            UsuariosPerfiles = new HashSet<UsuariosPerfile>();
        }

        public int IdPerfil { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<UsuariosPerfile> UsuariosPerfiles { get; set; }
    }
}
