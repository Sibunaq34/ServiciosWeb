using System;
using System.ServiceModel;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioRegistrarOferente
    {
        [OperationContract]
        ResultadoRegistrarEmpleado RegistrarOferente(
            string tipo_identificacion,
            string identificacion,
            string nombre_completo,
            string fecha_nacimiento,
            string correos_json,
            string telefonos_json,
            string codigo_puesto,
            string ruta_curriculum,
            string nombre_curriculum,
            string mime_curriculum,
            long tamanio_curriculum,
            int id_usuario_tecnico);
    }
}
