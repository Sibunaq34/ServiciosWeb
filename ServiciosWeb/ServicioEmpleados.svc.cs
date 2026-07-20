using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services;
using System;
using System.Configuration;

namespace ServiciosWeb
{
    public class ServicioEmpleados : IServicioEmpleados
    {
        private readonly EmpleadosService _service;

        public ServicioEmpleados()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            _service = new EmpleadosService(
                new EmpleadosRepository(factory));
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
    }
}
