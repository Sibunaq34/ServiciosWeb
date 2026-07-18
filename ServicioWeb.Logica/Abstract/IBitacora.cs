using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IBitacora
    {
        Task<IReadOnlyList<Bitacora>> ConsultarBitacoras(string usuario, string descripcion, int pagina, int tamanoPagina);
    }
}
