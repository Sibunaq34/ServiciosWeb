using System.Runtime.Serialization;

namespace ServiciosWeb.Modelo
{
    [DataContract]
    public class FaultDetail
    {
        [DataMember(Order = 1)]
        public string Codigo { get; set; } = string.Empty;

        [DataMember(Order = 2)]
        public string Mensaje { get; set; } = string.Empty;
    }
}
