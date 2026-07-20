using System;
using System.Configuration;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    public class ServicioUsuarios : IServicioUsuarios
    {
        private readonly UsuariosAdminServices _usuariosService;

        public ServicioUsuarios()
        {
            var connectionStringSettings =
                ConfigurationManager.ConnectionStrings["DefaultConnection"];

            if (connectionStringSettings == null ||
                string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new InvalidOperationException(
                    "No se encontró la cadena de conexión DefaultConnection."
                );
            }

            var connectionString =
                connectionStringSettings.ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            var repository =
                new UsuariosAdminRepository(factory);

            var encriptador =
                new EncriptadorAESServices();

            _usuariosService =
                new UsuariosAdminServices(
                    repository,
                    encriptador
                );
        }

        public bool RegistrarUsuario(RegistrarUsuario usuario)
        {
            return _usuariosService
                .Crear(usuario)
                .GetAwaiter()
                .GetResult();
        }
    }
}