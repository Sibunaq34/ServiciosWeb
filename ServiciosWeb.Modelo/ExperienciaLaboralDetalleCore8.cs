using System;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth
    // Representa una experiencia laboral dentro del detalle CORE8.
    public class ExperienciaLaboralDetalleCore8
    {
        public int IdExperiencia { get; set; }

        public int IdOferente { get; set; }

        public string NombreEmpresa { get; set; } = string.Empty;

        public string PuestoDesempenado { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}
