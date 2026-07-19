using Dapper;
using MySql.Data.MySqlClient;
using ServiciosMedicos.Entities;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    // Persona C - Kenneth
    // Consulta los datos relacionados sin modificar la base.
    public class DetalleOferenteRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public DetalleOferenteRepository(
            IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<DetalleOferenteCore8> ObtenerDetalleAsync(
            int idOferente)
        {
            try
            {
                using (var connection =
                    _dbConnectionFactory.CreateConnection())
                {
                    var detalle =
                        await connection.QuerySingleOrDefaultAsync<DetalleOferenteCore8>(
                            @"SELECT
                                o.id_oferente AS IdOferente,
                                p.id_persona AS IdPersona,
                                p.identificacion AS Identificacion,
                                p.tipo_identificacion AS TipoIdentificacion,
                                p.nombre_comple AS NombreCompleto,
                                p.fecha_naci AS FechaNacimiento,
                                o.fecha_regis AS FechaRegistro
                              FROM oferentes o
                              INNER JOIN personas p ON p.id_persona = o.id_persona
                              WHERE o.id_oferente = @IdOferente;",
                            new { IdOferente = idOferente });

                    if (detalle == null)
                    {
                        return null;
                    }

                    detalle.Correos =
                        (await connection.QueryAsync<string>(
                            @"SELECT correo
                              FROM oferente_correo
                              WHERE id_oferente = @IdOferente
                              ORDER BY id_of_correo;",
                            new { IdOferente = idOferente }))
                        .ToList();

                    detalle.Telefonos =
                        (await connection.QueryAsync<string>(
                            @"SELECT telefono
                              FROM oferente_telf
                              WHERE id_oferente = @IdOferente
                              ORDER BY id_of_telefono;",
                            new { IdOferente = idOferente }))
                        .ToList();

                    detalle.PreparacionAcademica =
                        (await connection.QueryAsync<PreparacionAcademicaDetalleCore8>(
                            @"SELECT
                                pa.id_pre_academica AS IdPreparacion,
                                pa.id_oferente AS IdOferente,
                                pa.id_insti_edu AS IdInstitucion,
                                ie.codigo_insti AS CodigoInstitucion,
                                ie.nombre AS NombreInstitucion,
                                pa.titulo_obtenido AS Titulo,
                                pa.fecha_inicio AS FechaInicio,
                                pa.fecha_fin AS FechaFin
                              FROM prepara_academica pa
                              INNER JOIN institu_educa ie ON ie.id_insti_edu = pa.id_insti_edu
                              WHERE pa.id_oferente = @IdOferente
                              ORDER BY pa.fecha_inicio DESC, pa.titulo_obtenido ASC;",
                            new { IdOferente = idOferente }))
                        .ToList();

                    detalle.ExperienciaLaboral =
                        (await connection.QueryAsync<ExperienciaLaboralDetalleCore8>(
                            @"SELECT
                                id_experiencia AS IdExperiencia,
                                id_oferente AS IdOferente,
                                nombre_empresa AS NombreEmpresa,
                                puesto_desempenado AS PuestoDesempenado,
                                fecha_inicio AS FechaInicio,
                                fecha_fin AS FechaFin
                              FROM experiencia_laboral
                              WHERE id_oferente = @IdOferente
                              ORDER BY fecha_inicio DESC, nombre_empresa ASC;",
                            new { IdOferente = idOferente }))
                        .ToList();

                    detalle.Participaciones =
                        (await connection.QueryAsync<ParticipacionConcursoDetalleCore8>(
                            @"SELECT
                                oc.id_of_concurso AS IdParticipacion,
                                c.id_concursos AS IdConcurso,
                                c.codigo_concurso AS CodigoConcurso,
                                c.nombre_concurso AS NombreConcurso,
                                c.fecha_inicio AS FechaInicio,
                                c.fecha_fin AS FechaFin,
                                c.estado_concur AS Estado,
                                oc.fecha_asigna AS FechaAsignacion
                              FROM oferente_concur oc
                              INNER JOIN concursos c ON c.id_concursos = oc.id_concursos
                              WHERE oc.id_oferente = @IdOferente
                              ORDER BY oc.fecha_asigna DESC, c.nombre_concurso ASC;",
                            new { IdOferente = idOferente }))
                        .ToList();

                    return detalle;
                }
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException(
                    "No fue posible consultar el detalle del oferente.",
                    ex);
            }
            catch (DataException ex)
            {
                throw new InvalidOperationException(
                    "No fue posible mapear el detalle del oferente.",
                    ex);
            }
        }
    }
}
