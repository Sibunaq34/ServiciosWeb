using Dapper;
using DocumentFormat.OpenXml.InkML;
using ServiciosMedicos.Entities;
using System.Data;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public class SeguridadRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;


        public SeguridadRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }


        public async Task<SeguridadLog> ObtenerUsuario(string username)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<SeguridadLog>("ValidarUsuario", new { pUsuario = username }, commandType: CommandType.StoredProcedure
                );
            }
        }


        public async Task RegistrarIntentoFallido(
            int usuario,
            int intentos)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    "RegistrarIntentoFallido",
                    new
                    {
                        pIdUsuario = usuario,
                        pIntentos = intentos
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}