using System;
using System.Configuration;
using System.ServiceModel;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using ServiciosWeb.Modelo;

namespace ServiciosWeb
{
    public class ServicioLogIn : IServicioLogIn
    {
        private readonly AutenticacionServices _authService;

        public ServicioLogIn()
        {
            var connectionStringSettings =
                ConfigurationManager.ConnectionStrings["DefaultConnection"];

            if (connectionStringSettings == null ||
                string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión DefaultConnection.");
            }

            var connectionString =
                connectionStringSettings.ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            var repository =
                new SeguridadRepository(factory);

            var encriptador =
                new EncriptadorAESServices();

            _authService =
                new AutenticacionServices(repository, encriptador);
        }

        public ResultadoAutenticacion Login(string usuario, string password)
        {
            try
            {
                return _authService
                    .Login(usuario, password)
                    .GetAwaiter()
                    .GetResult();
            }
            catch (Exception)
            {
                var faultDetail = new FaultDetail
                {
                    Codigo = "LoginError",
                    Mensaje = "No fue posible procesar la autenticación."
                };

                throw new FaultException<FaultDetail>(
                    faultDetail,
                    new FaultReason(faultDetail.Mensaje)
                );
            }
        }
    }
}