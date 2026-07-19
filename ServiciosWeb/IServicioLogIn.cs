using System.ServiceModel;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioLogIn
    {
        [OperationContract]
        ResultadoAutenticacion Login(string usuario, string password);
    }
}
