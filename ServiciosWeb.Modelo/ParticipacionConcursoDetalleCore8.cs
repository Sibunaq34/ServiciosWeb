using System;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth
    // Describe una participacion del oferente en concursos.
    public class ParticipacionConcursoDetalleCore8
    {
        public int IdParticipacion { get; set; }

        public int IdConcurso { get; set; }

        public string CodigoConcurso { get; set; } = string.Empty;

        public string NombreConcurso { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } = string.Empty;

        public DateTime FechaAsignacion { get; set; }
    }
}
