using System;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth
    // Representa una preparacion academica dentro del detalle CORE8.
    public class PreparacionAcademicaDetalleCore8
    {
        public int IdPreparacion { get; set; }

        public int IdOferente { get; set; }

        public int IdInstitucion { get; set; }

        public string CodigoInstitucion { get; set; } = string.Empty;

        public string NombreInstitucion { get; set; } = string.Empty;

        public string Titulo { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }
    }
}
