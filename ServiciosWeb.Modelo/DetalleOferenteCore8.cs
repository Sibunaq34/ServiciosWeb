using System;
using System.Collections.Generic;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth
    // Agrupa el detalle completo confirmado para CORE8.
    public class DetalleOferenteCore8
    {
        public int IdOferente { get; set; }

        public int IdPersona { get; set; }

        public string Identificacion { get; set; } = string.Empty;

        public string TipoIdentificacion { get; set; } = string.Empty;

        public string NombreCompleto { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaRegistro { get; set; }

        public List<string> Correos { get; set; } = new List<string>();

        public List<string> Telefonos { get; set; } = new List<string>();

        public List<PreparacionAcademicaDetalleCore8> PreparacionAcademica { get; set; } =
            new List<PreparacionAcademicaDetalleCore8>();

        public List<ExperienciaLaboralDetalleCore8> ExperienciaLaboral { get; set; } =
            new List<ExperienciaLaboralDetalleCore8>();

        public List<ParticipacionConcursoDetalleCore8> Participaciones { get; set; } =
            new List<ParticipacionConcursoDetalleCore8>();
    }
}
