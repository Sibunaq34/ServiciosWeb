using ServiciosMedicos.Entities;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    // Persona C - Kenneth
    // Define el acceso a datos requerido por CORE8.
    public interface IDetalleOferenteRepository
    {
        Task<DetalleOferenteCore8> ObtenerDetalleAsync(
            int idOferente);
    }
}
