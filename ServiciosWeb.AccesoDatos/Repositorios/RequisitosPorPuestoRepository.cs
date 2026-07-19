using Dapper;
using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public class RequisitosPorPuestoRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public RequisitosPorPuestoRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<RequisitoPuestoDto>> ListarRequisitosPorPuesto(string codigoPuesto)
        {
            using (var connection = _dbConnectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<RequisitoPuestoDto>(
                    "sp_ObtenerRequisitosPorPuesto",
                    new { pCodigoPuesto = codigoPuesto },
                    commandType: CommandType.StoredProcedure);

                return result ?? Enumerable.Empty<RequisitoPuestoDto>();
            }
        }
    }
}
