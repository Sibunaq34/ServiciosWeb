using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IUsuariosAdmin
    {
        Task<IEnumerable<UsuarioAdmin>> Listar();
        Task<UsuarioAdmin> ObtenerPorId(int idUsuario);
        Task<bool> Crear(RegistrarUsuario usuario);
        Task CambiarEstado(int idUsuario, bool activo);
        Task Eliminar(int idUsuario);
    }
}