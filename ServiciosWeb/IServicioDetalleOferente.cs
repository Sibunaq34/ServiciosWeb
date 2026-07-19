using ServiciosMedicos.Entities;
using System.ServiceModel;

namespace ServiciosWeb
{
    // Persona C - Kenneth
    // Expone el contrato SOAP para consultar CORE8.
    [ServiceContract]
    public interface IServicioDetalleOferente
    {
        [OperationContract]
        ResultadoDetalleOferente ObtenerDetalleOferente(
            int idOferente);
    }
}
