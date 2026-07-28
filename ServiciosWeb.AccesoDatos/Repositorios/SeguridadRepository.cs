using Dapper;
using ServiciosMedicos.Entities;
using System.Data;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public class SeguridadRepository : ISeguridadRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public SeguridadRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<SeguridadLog> ObtenerUsuario(string usuario)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<SeguridadLog>(
                    "ValidarUsuario",
                    new { pUsuario = usuario },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task RegistrarIntentoFallido(int idUsuario)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "RegistrarIntentoFallido",
                    new { pIdUsuario = idUsuario },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ReiniciarIntentosFallidos(int idUsuario)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "ReiniciarIntentosFallidos",
                    new { pIdUsuario = idUsuario },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ActualizarPasswordCifradaUsuario(int idUsuario, string passwordCifrada)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "ActualizarPasswordCifradaUsuario",
                    new
                    {
                        pIdUsuario = idUsuario,
                        pPasswordCifrada = passwordCifrada
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
