
using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    // Persona C - Kenneth: Contrato de servicio para OFE3.
    public interface IPreparacionAcademica
    {
        Task<IReadOnlyList<PreparacionAcademica>> ListarPorOferenteAsync(
            int idOferente,
            int pagina,
            int tamanoPagina,
            int idUsuario);

        Task<PreparacionAcademica> ObtenerAsync(
            int idPreparacion,
            int idUsuario);

        Task<int> CrearAsync(
            PreparacionAcademica preparacion,
            int idUsuario);

        Task ActualizarAsync(
            PreparacionAcademica preparacion,
            int idUsuario);

        Task EliminarAsync(
            int idPreparacion,
            int idUsuario);
    }
}
