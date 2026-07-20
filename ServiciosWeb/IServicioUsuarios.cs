using System.ServiceModel;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioUsuarios
    {
        [OperationContract]
        bool RegistrarUsuario(RegistrarUsuario usuario);
    }
}