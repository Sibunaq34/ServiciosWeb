using System;
using System.Configuration;
using System.ServiceModel;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    public class ServicioRegistrarOferente : IServicioRegistrarOferente
    {
        private readonly RegistrarOferentePuestoService _service;

        public ServicioRegistrarOferente()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            var repo = new RegistrarOferentePuestoRepository(factory);
            _service = new RegistrarOferentePuestoService(repo);
        }

        public ResultadoRegistrarEmpleado RegistrarOferente(
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
            int id_usuario_tecnico)
        {
            try
            {
                var resultado = _service.RegistrarAsync(
                    tipo_identificacion,
                    identificacion,
                    nombre_completo,
                    fecha_nacimiento,
                    correos_json,
                    telefonos_json,
                    codigo_puesto,
                    ruta_curriculum,
                    nombre_curriculum,
                    mime_curriculum,
                    tamanio_curriculum,
                    id_usuario_tecnico).Result;

                return resultado;
            }
            catch (Exception)
            {
                return new ResultadoRegistrarEmpleado
                {
                    Exito = false,
                    Codigo = "INTERNAL_ERROR",
                    Mensaje = "No fue posible registrar el oferente."
                };
            }
        }
    }
}
