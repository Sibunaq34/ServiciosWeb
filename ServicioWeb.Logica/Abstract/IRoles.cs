using ServiciosMedicos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{

    public interface IRoles
    {
        Task<IEnumerable<Rol>> Listar();
        Task<Rol> ObtenerPorId(int idRol);
        Task Crear(Rol rol);
        Task Actualizar(Rol rol);
        Task Eliminar(int idRol);
        Task<IEnumerable<Pantalla>> ListarPantallasPorRol(int idRol);

        Task GuardarPantallasRol(
            int idRol,
            List<int> pantallasSeleccionadas);
    }
}