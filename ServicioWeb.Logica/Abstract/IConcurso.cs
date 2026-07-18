using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    // Persona C - Kenneth: Contrato de servicio para OFE2.
    public interface IConcurso
    {
        Task<IReadOnlyList<Concurso>> ListarAsync(
            int pagina,
            int tamanoPagina,
            int idUsuario);

        Task<Concurso> ObtenerAsync(
            int idConcurso,
            int idUsuario);


        Task<int> CrearAsync(
            Concurso concurso,
            int idUsuario);

        Task ActualizarAsync(
            Concurso concurso,
            int idUsuario);

        Task CambiarEstadoAsync(
            int idConcurso,
            string estado,
            int idUsuario);

        Task EliminarAsync(
            int idConcurso,
            int idUsuario);
    }
}
