using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioPuestos
    {
        [OperationContract]
        List<Puesto> ListarPuestos();
    }
}