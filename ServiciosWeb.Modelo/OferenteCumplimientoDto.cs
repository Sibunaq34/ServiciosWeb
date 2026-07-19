using System.Runtime.Serialization;

namespace ServiciosMedicos.Entities
{
    [DataContract]
    public class OferenteCumplimientoDto
    {
        [DataMember(Order = 1)]
        public int IdOferente { get; set; }

        [DataMember(Order = 2)]
        public string NombreCompleto { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Identificacion { get; set; } = string.Empty;
    }
}
