using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System.Configuration;

namespace ServiciosWeb
{
    // Persona C - Kenneth
    // Expone la consulta SOAP del detalle del oferente.
    public class ServicioDetalleOferente : IServicioDetalleOferente
    {
        private readonly IDetalleOferente _detalleService;

        public ServicioDetalleOferente()
            : this(CrearServicio())
        {
        }

        internal ServicioDetalleOferente(
            IDetalleOferente detalleService)
        {
            _detalleService = detalleService;
        }

        public ResultadoDetalleOferente ObtenerDetalleOferente(
            int idOferente)
        {
            return _detalleService
                .ObtenerDetalleAsync(idOferente)
                .Result;
        }

        private static IDetalleOferente CrearServicio()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la cadena de conexión 'DefaultConnection'."
                );
            }

            return new DetalleOferenteService(connectionString);
        }
    }
}
