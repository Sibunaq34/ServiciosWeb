using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using System;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class RegistrarOferentePuestoService
    {
        private readonly RegistrarOferentePuestoRepository _repository;

        public RegistrarOferentePuestoService(RegistrarOferentePuestoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultadoRegistrarEmpleado> RegistrarAsync(
            string tipoIdentificacion,
            string identificacion,
            string nombreCompleto,
            string fechaNacimiento,
            string correosJson,
            string telefonosJson,
            string codigoPuesto,
            string rutaCurriculum,
            string nombreCurriculum,
            string mimeCurriculum,
            long tamanioCurriculum,
            int idUsuarioTecnico)
        {
            try
            {
                await _repository.RegistrarAsync(
                    tipoIdentificacion,
                    identificacion,
                    nombreCompleto,
                    fechaNacimiento,
                    correosJson,
                    telefonosJson,
                    codigoPuesto,
                    rutaCurriculum,
                    nombreCurriculum,
                    mimeCurriculum,
                    tamanioCurriculum,
                    idUsuarioTecnico);

                return new ResultadoRegistrarEmpleado
                {
                    Exito = true
                };
            }
            catch (Exception)
            {
                return new ResultadoRegistrarEmpleado
                {
                    Exito = false,
                    Codigo = "DB_ERROR",
                    Mensaje = "No fue posible registrar el oferente para el puesto."
                };
            }
        }
    }
}
