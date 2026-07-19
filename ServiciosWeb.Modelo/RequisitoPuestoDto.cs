using System.Runtime.Serialization;

namespace ServiciosMedicos.Entities
{
    [DataContract]
    public class RequisitoPuestoDto
    {
        [DataMember(Order = 1)]
        public int IdRequisito { get; set; }

        [DataMember(Order = 2)]
        public string NombreRequisito { get; set; } = string.Empty;
    }
}
