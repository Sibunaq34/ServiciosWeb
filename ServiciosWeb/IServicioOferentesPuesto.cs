using System.Collections.Generic;
using System.ServiceModel;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    [ServiceContract]
    public interface IServicioOferentesPuesto
    {
        [OperationContract]
        List<RequisitoPuestoDto> ListarRequisitosPorPuesto(string codigoPuesto);

        [OperationContract]
        List<OferenteCumplimientoDto> ListarOferentesPorPuesto(string codigoPuesto);

        [OperationContract]
        List<OferenteCumplimientoDto> ListarTodosLosOferentes();
    }
}