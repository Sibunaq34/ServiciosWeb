using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios_Medicos.Repository
{
    public interface IPuestosRepository
    {
        Task<IEnumerable<Puesto>> ListarPuestos();
        Task<Puesto> ObtenerPuesto(int idPuesto);
        Task<bool> InsertarPuesto(Puesto puesto);
        Task<bool> ActualizarPuesto(Puesto puesto);
        Task<bool> EliminarPuesto(int idPuesto);
    }
}