using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class OferentesPorPuestoService : IOferentesPorPuesto
    {
        private readonly OferentesPorPuestoRepository _oferentesRepository;
        private readonly RequisitosPorPuestoRepository _requisitosRepository;

        public OferentesPorPuestoService(
            OferentesPorPuestoRepository oferentesRepository,
            RequisitosPorPuestoRepository requisitosRepository)
        {
            _oferentesRepository = oferentesRepository;
            _requisitosRepository = requisitosRepository;
        }

        public async Task<IEnumerable<OferenteCumplimientoDto>> ListarOferentesPorPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
            {
                return Enumerable.Empty<OferenteCumplimientoDto>();
            }

            var codigo = codigoPuesto.Trim();
            var resultados = await _oferentesRepository.ListarOferentesPorPuesto(codigo);
            return resultados ?? Enumerable.Empty<OferenteCumplimientoDto>();
        }

        public async Task<IEnumerable<RequisitoPuestoDto>> ListarRequisitosPorPuesto(string codigoPuesto)
        {
            if (string.IsNullOrWhiteSpace(codigoPuesto))
            {
                return Enumerable.Empty<RequisitoPuestoDto>();
            }

            var codigo = codigoPuesto.Trim();
            var resultados = await _requisitosRepository.ListarRequisitosPorPuesto(codigo);
            return resultados ?? Enumerable.Empty<RequisitoPuestoDto>();
        }
    }
}
