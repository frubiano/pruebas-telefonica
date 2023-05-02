using System;
using System.Collections.Generic;

namespace ahorra_marcket.Modelos
{
    public partial class UsuariosPerfile
    {
        public int IdUserPerf { get; set; }
        public int IdPerfil { get; set; }
        public int IdUser { get; set; }

        public virtual Perfile IdPerfilNavigation { get; set; } = null!;
        public virtual Usuario IdUserNavigation { get; set; } = null!;
    }
}
