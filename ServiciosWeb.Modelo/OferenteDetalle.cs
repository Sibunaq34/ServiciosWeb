using ServiciosMedicos.Entities;
using System;
using System.Collections.Generic;

namespace ServiciosMedicos.Entities
{
    // Persona C - Kenneth: DTO de detalle para OFE1.
    public class OferenteDetalle : Oferente
    {
        public DateTime FechaRegistro { get; set; }

        public IReadOnlyList<Concurso> Concursos { get; set; } =
            Array.Empty<Concurso>();
    }
}
