using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ServiciosMedicos.Services
{
    public class EmpleadosService : IEmpleados
    {
        private readonly EmpleadosRepository _empleadosBD;

        public EmpleadosService(
            EmpleadosRepository empleadosBD)
        {
            _empleadosBD = empleadosBD;
        }

        public async Task<IEnumerable<OferenteCombo>> ListarOferentes()
        {
            return await _empleadosBD.ListarOferentes();
        }

        public async Task<IEnumerable<Puesto>> ListarPuestos()
        {
            return await _empleadosBD.ListarPuestos();
        }

        public async Task<IEnumerable<EmpleadoCombo>> ListarEmpleados()
        {
            return await _empleadosBD.ListarEmpleados();
        }

        public async Task<bool> ContratarEmpleado(EmpleadoContratacion empleado)
        {
            return await _empleadosBD
                .ContratarEmpleado(empleado);
        }

        public async Task<ResultadoRegistrarEmpleado> RegistrarEmpleado(
            EntradaRegistrarEmpleado solicitud)
        {
            if (solicitud == null || solicitud.IdOferente <= 0 ||
                string.IsNullOrWhiteSpace(solicitud.CodigoPuesto) ||
                solicitud.CodigoPuesto.Length > 20 ||
                solicitud.IdUsuario <= 0 ||
                (solicitud.IdJefatura.HasValue &&
                 solicitud.IdJefatura.Value <= 0))
            {
                return Error(
                    "VALIDATION_ERROR",
                    "La solicitud contiene datos inválidos.");
            }

            var validacion = await _empleadosBD
                .ValidarContratacion(solicitud);

            if (!string.IsNullOrEmpty(validacion))
                return Error(validacion, MensajePara(validacion));

            try
            {
                var creado = await _empleadosBD
                    .RegistrarEmpleado(solicitud);

                return creado
                    ? new ResultadoRegistrarEmpleado
                    {
                        Exito = true,
                        Codigo = "EMPLOYEE_CREATED",
                        Mensaje = "El empleado fue creado correctamente."
                    }
                    : Error(
                        "DATABASE_ERROR",
                        "La base de datos no confirmó la creación.");
            }
            catch (Exception ex)
            {
                // Return the underlying exception message to help debugging during development.
                // Remove or change this in production to avoid leaking internal details.
                return Error(
                    "DATABASE_ERROR",
                    "No fue posible guardar el empleado. Detalle: " + ex.Message);
            }
        }

        public Task<bool> OferenteEsEmpleado(int idOferente)
        {
            return _empleadosBD.OferenteEsEmpleado(idOferente);
        }

        private static ResultadoRegistrarEmpleado Error(
            string codigo,
            string mensaje)
        {
            return new ResultadoRegistrarEmpleado
            {
                Exito = false,
                Codigo = codigo,
                Mensaje = mensaje
            };
        }

        private static string MensajePara(string codigo)
        {
            switch (codigo)
            {
                case "OFFERER_NOT_FOUND":
                    return "El oferente no existe.";
                case "EMPLOYEE_ALREADY_EXISTS":
                    return "El oferente ya fue convertido en empleado.";
                case "POSITION_NOT_FOUND":
                    return "El puesto no existe.";
                case "MANAGER_NOT_FOUND":
                    return "La jefatura indicada no existe.";
                default:
                    return "No fue posible validar la solicitud.";
            }
        }

    }
}