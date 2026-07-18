using System.Configuration;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    public class ServicioLogIn : IServicioLogIn
    {
        private readonly AutenticacionServices _authService;

        private readonly UsuariosAdminServices _usuariosAdmin;


        public ServicioLogIn()
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            IDbConnectionFactory factory =
                new DbConnectionFactory(connectionString);

            var repository = new SeguridadRepository(factory);
            var encriptador = new EncriptadorAESServices();

            _authService =
                new AutenticacionServices(
                    repository,
                    new EncriptadorAESServices());

            var usuariosAdminRepository = new UsuariosAdminRepository(factory);

            _usuariosAdmin =
                new UsuariosAdminServices(
                    usuariosAdminRepository,
                    encriptador);
        }

        public SeguridadLog Login(string usuario, string password)
        {
            return _authService.Login(usuario, password).Result;
        }

        public bool Crear(RegistrarUsuario usuario)
        {
            return _usuariosAdmin.Crear(usuario).Result;
        }
    }
}