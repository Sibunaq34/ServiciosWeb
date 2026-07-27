using ServiciosMedicos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiciosMedicos.Services.Abstract
{
    public interface IEmpleados
    {
        Task<IEnumerable<OferenteCombo>>
            ListarOferentes();

        Task<IEnumerable<Puesto>>
            ListarPuestos();

        Task<IEnumerable<EmpleadoCombo>>
            ListarEmpleados();

        Task<bool>
            ContratarEmpleado(
            EmpleadoContratacion empleado);

        Task<ResultadoRegistrarEmpleado>
            RegistrarEmpleado(EntradaRegistrarEmpleado solicitud);

        Task<bool>
            OferenteEsEmpleado(int idOferente);
    }
}