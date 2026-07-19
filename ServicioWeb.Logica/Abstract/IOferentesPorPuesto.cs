using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IOferentesPorPuesto
    {
        Task<IEnumerable<OferenteCumplimientoDto>> ListarOferentesPorPuesto(string codigoPuesto);

        Task<IEnumerable<RequisitoPuestoDto>> ListarRequisitosPorPuesto(string codigoPuesto);
    }
}
