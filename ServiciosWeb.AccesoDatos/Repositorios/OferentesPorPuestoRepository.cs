using Dapper;
using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public class OferentesPorPuestoRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public OferentesPorPuestoRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<OferenteCumplimientoDto>> ListarOferentesPorPuesto(string codigoPuesto)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<OferenteCumplimientoDto>(
                    "sp_ObtenerOferentesPorPuesto",
                    new { pCodigoPuesto = codigoPuesto },
                    commandType: CommandType.StoredProcedure);

                return result ?? Enumerable.Empty<OferenteCumplimientoDto>();
            }
        }

        public async Task<IEnumerable<OferenteCumplimientoDto>> ListarTodos()
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<OferenteCumplimientoDto>(
                    "SP_ListarOferentes",
                    commandType: CommandType.StoredProcedure);

                return result ?? Enumerable.Empty<OferenteCumplimientoDto>();
            }
        }
    }
}
