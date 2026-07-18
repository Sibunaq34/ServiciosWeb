using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMedicos.Entities
{

        public class RegistrarUsuario
    {
            public string Usuario { get; set; }
            public string NombreCompleto { get; set; }
            public string Correo { get; set; }
            public string Password { get; set; }
            public bool Activo { get; set; }

            public string Estado { get; set; } = "Activo";
            public int IdRol { get; set; }
        }
    
}
