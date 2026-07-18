using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IAccionesPersonal
    {
        Task<IEnumerable<AccionPersonal>>
            ListarAcciones();

        Task<bool>
            InsertarAccion(
            AccionPersonal accion);

        Task<bool>
            EliminarAccion(
            int idAccion);
    }
}
