using ServiciosMedicos.Entities;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IUsuario
    {
        Task<ResultadoAutenticacion> Login(
            string usuario,
            string password);
    }
}