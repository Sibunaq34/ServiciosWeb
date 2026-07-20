using ServiciosMedicos.Entities;
using System.ServiceModel;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioEmpleados
    {
        [OperationContract]
        ResultadoRegistrarEmpleado RegistrarEmpleado(
            EntradaRegistrarEmpleado entrada);

        [OperationContract]
        bool OferenteEsEmpleado(int idOferente);
    }
}
