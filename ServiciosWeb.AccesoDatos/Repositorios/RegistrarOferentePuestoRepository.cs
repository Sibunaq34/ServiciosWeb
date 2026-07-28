using Dapper;
using ServiciosMedicos.Entities;
using System.Data;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public class RegistrarOferentePuestoRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RegistrarOferentePuestoRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task RegistrarAsync(
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
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "sp_aut3_registrar_oferente_puesto",
                    new
                    {
                        p_tipo_identificacion = tipoIdentificacion,
                        p_identificacion = identificacion,
                        p_nombre_completo = nombreCompleto,
                        p_fecha_nacimiento = fechaNacimiento,
                        p_correos_json = correosJson,
                        p_telefonos_json = telefonosJson,
                        p_codigo_puesto = codigoPuesto,
                        p_ruta_curriculum = rutaCurriculum,
                        p_nombre_curriculum = nombreCurriculum,
                        p_mime_curriculum = mimeCurriculum,
                        p_tamanio_curriculum = tamanioCurriculum,
                        p_id_usuario_tecnico = idUsuarioTecnico
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
