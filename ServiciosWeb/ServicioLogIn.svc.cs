using System;
using System.Configuration;
using System.ServiceModel;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using ServiciosWeb.Modelo;

namespace ServiciosWeb
{
    public class ServicioLogIn : IServicioLogIn
    {
        private readonly IUsuario _authService;

        public ServicioLogIn()
            : this(CrearServicio())
        {
        }

        internal ServicioLogIn(IUsuario authService)
        {
            _authService = authService ??
                throw new ArgumentNullException(nameof(authService));
        }

        private static IUsuario CrearServicio()
        {
            var connectionStringSettings =
                ConfigurationManager.ConnectionStrings["DefaultConnection"];

            if (connectionStringSettings == null ||
                string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión DefaultConnection.");
            }

            return new AutenticacionServices(
                connectionStringSettings.ConnectionString);
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
