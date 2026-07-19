using System.Runtime.Serialization;

namespace ServiciosMedicos.Entities
{
    [DataContract]
    public class ResultadoAutenticacion
    {
        [DataMember(Order = 1)]
        public bool Exito { get; set; }

        [DataMember(Order = 2)]
        public string Mensaje { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public int IdUsuario { get; set; }

        [DataMember(Order = 4)]
        public string Usuario { get; set; } = string.Empty;

        [DataMember(Order = 5)]
        public string NombreCompleto { get; set; } = string.Empty;

        [DataMember(Order = 6)]
        public int IdRol { get; set; }

        [DataMember(Order = 7)]
        public string NombreRol { get; set; } = string.Empty;

        [DataMember(Order = 8)]
        public string Estado { get; set; } = string.Empty;
    }
}
