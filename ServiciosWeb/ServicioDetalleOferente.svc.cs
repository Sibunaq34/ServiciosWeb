using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using System.Configuration;

namespace ServiciosWeb
{
    // Persona C - Kenneth
    // Expone la consulta SOAP del detalle del oferente.
    public class ServicioDetalleOferente : IServicioDetalleOferente
    {
        private readonly DetalleOferenteService _detalleService;

        public ServicioDetalleOferente()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            var repository =
                new DetalleOferenteRepository(factory);

            _detalleService =
                new DetalleOferenteService(repository);
        }

        public ResultadoDetalleOferente ObtenerDetalleOferente(
            int idOferente)
        {
            return _detalleService
                .ObtenerDetalleAsync(idOferente)
                .Result;
        }
    }
}
