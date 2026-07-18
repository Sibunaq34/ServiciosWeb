using System;
using System.Collections.Generic;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth: Entidad para OFE1 Oferentes.
    public class Oferente
    {
        public int IdOferente { get; set; }

        public int IdPersona { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string TipoIdentificacion { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public DateTime? FechaNacimiento { get; set; }

        public List<string> Correos { get; set; } = new List<string>();

        public List<string> Telefonos { get; set; } = new List<string>();

        public List<int> ConcursosIds { get; set; } = new List<int>();
    }
}
