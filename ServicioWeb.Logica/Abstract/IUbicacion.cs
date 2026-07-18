using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IUbicacion
    {
        Task CargarUbicaciones(IFormFile archivo, int idusuario);
    }
}