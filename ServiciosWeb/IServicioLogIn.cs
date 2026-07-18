using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiciosWeb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServicioLogIn" in both code and config file together.
    [ServiceContract]
    public interface IServicioLogIn
    {
        [OperationContract]
        ServiciosMedicos.Entities.SeguridadLog Login(string usuario, string password);

        [OperationContract]
        bool Crear(ServiciosMedicos.Entities.RegistrarUsuario usuario);
    }
}
