using ServiciosMedicos.Entities;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IUsuario
    {

        Task<SeguridadLog> Login(
            string usuario,
            string password);
    }
}