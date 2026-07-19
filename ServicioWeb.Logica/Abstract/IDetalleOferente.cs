using ServiciosMedicos.Entities;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    // Persona C - Kenneth
    // Define la consulta de detalle del oferente para CORE8.
    public interface IDetalleOferente
    {
        Task<ResultadoDetalleOferente> ObtenerDetalleAsync(
            int idOferente);
    }
}
