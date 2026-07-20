using System.ServiceModel;
using ServiciosMedicos.Entities;
using ServiciosWeb.Modelo;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioLogIn
    {
        [OperationContract]
        [FaultContract(typeof(FaultDetail))]
        ResultadoAutenticacion Login(string usuario, string password);
    }
}
