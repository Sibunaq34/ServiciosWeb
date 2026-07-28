using ServiciosMedicos.Entities;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public interface ISeguridadRepository
    {
        Task<SeguridadLog> ObtenerUsuario(string usuario);

        Task RegistrarIntentoFallido(int idUsuario);

        Task ReiniciarIntentosFallidos(int idUsuario);

        Task ActualizarPasswordCifradaUsuario(int idUsuario, string passwordCifrada);
    }
}
