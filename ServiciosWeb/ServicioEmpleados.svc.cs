using ServiciosMedicos.Entities;
using ServiciosMedicos.Services;
using ServiciosMedicos.Services.Abstract;
using System;
using System.Configuration;

namespace ServiciosWeb
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        private readonly IEmpleados _service;

        public ServicioEmpleados()
            : this(CrearServicio())
        {
        }

        internal ServicioEmpleados(IEmpleados service)
        {
            _service = service;
        }

        public ResultadoRegistrarEmpleado RegistrarEmpleado(
            EntradaRegistrarEmpleado entrada)
        {
            try
            {
                return _service.RegistrarEmpleado(entrada).Result;
            }
            catch (Exception)
            {
                return new ResultadoRegistrarEmpleado
                {
                    Exito = false,
                    Codigo = "INTERNAL_ERROR",
                    Mensaje = "No fue posible registrar el empleado."
                };
            }
        }

        public bool OferenteEsEmpleado(int idOferente)
        {
            if (idOferente <= 0)
                return false;

            try
            {
                return _service.OferenteEsEmpleado(idOferente).Result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static IEmpleados CrearServicio()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                ?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la cadena de conexión 'DefaultConnection'."
                );
            }

            return new EmpleadosService(connectionString);
        }
    }
}