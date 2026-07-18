
using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IPantallas
    {
        Task<IEnumerable<Pantalla>> Listar();

        Task<Pantalla> ObtenerPorId(int idPantalla);

        Task Crear(Pantalla pantalla);

        Task Actualizar(Pantalla pantalla);

        Task Eliminar(int idPantalla);

    }
}