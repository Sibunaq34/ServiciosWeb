using System.Runtime.Serialization;

namespace ServiciosMedicos.Entities
{
    [DataContract]
    public class ResultadoRegistrarEmpleado
    {
        [DataMember(Order = 1)]
        public bool Exito { get; set; }

        [DataMember(Order = 2)]
        public string Codigo { get; set; } = string.Empty;

        [DataMember(Order = 3)]
        public string Mensaje { get; set; } = string.Empty;
    }
}
