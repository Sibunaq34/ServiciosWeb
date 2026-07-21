using System;
using System.Collections.Generic;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth
    // Agrupa el detalle registrado por AUT3 para CORE8.
    public class DetalleOferenteCore8
    {
        public int IdOferente { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string TipoIdentificacion { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public List<string> Correos { get; set; } = new List<string>();

        public List<string> Telefonos { get; set; } = new List<string>();

        public PuestoPostulacionDetalleCore8 Puesto { get; set; }

        public CurriculumDetalleCore8 Curriculum { get; set; }
    }
}
