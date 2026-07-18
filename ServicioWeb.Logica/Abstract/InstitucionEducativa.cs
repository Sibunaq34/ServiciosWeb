
using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    // Persona C - Kenneth: Contrato de servicio para GEN5.
    public interface IInstitucionEducativa
    {
        Task<IReadOnlyList<InstitucionEducativa>> ListarAsync(
            int pagina,
            int tamanoPagina,
            int idUsuario);

        Task<InstitucionEducativa> ObtenerAsync(
            int idInstitucion,
            int idUsuario);

        Task<int> CrearAsync(
            InstitucionEducativa institucion,
            int idUsuario);

        Task ActualizarAsync(
            InstitucionEducativa institucion,
            int idUsuario);

        Task EliminarAsync(
            int idInstitucion,
            int idUsuario);
    }
}
