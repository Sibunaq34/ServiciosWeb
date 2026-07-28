using System;
using System.Configuration;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;

namespace ServiciosWeb
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly IUsuariosAdmin _usuariosService;

        public ServicioUsuarios()
            : this(CrearServicio())
        {
        }

        internal ServicioUsuarios(IUsuariosAdmin usuariosService)
        {
            _usuariosService = usuariosService;
        }

        public bool RegistrarUsuario(RegistrarUsuario usuario)
        {
            return _usuariosService
                .Crear(usuario)
                .GetAwaiter()
                .GetResult();
        }

        private static IUsuariosAdmin CrearServicio()
        {
            var connectionStringSettings =
                ConfigurationManager.ConnectionStrings["DefaultConnection"];

            if (connectionStringSettings == null ||
                string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la cadena de conexión DefaultConnection."
                );
            }

            var connectionString = connectionStringSettings.ConnectionString;

            return new UsuariosAdminServices(connectionString);
        }
    }
}
