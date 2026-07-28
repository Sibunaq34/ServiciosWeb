
using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class UsuariosAdminServices : IUsuariosAdmin
    {
        private readonly UsuariosAdminRepository _usuariosBD;
        private readonly EncriptadorAESServices _encriptadorAES;
  

        internal UsuariosAdminServices(UsuariosAdminRepository usuariosBD, EncriptadorAESServices encriptadorAES)
        {
            _usuariosBD = usuariosBD;
            _encriptadorAES = encriptadorAES;
        }

        // Convenience constructor that accepts a connection string and creates
        // the required repository and encrypter internally so the WCF layer
        // can remain free of data access dependencies.
        public UsuariosAdminServices(string connectionString)
        {
            IDbConnectionFactory factory = new DbConnectionFactory(connectionString);
            _usuariosBD = new UsuariosAdminRepository(factory);
            _encriptadorAES = new EncriptadorAESServices();
        }

        public Task<IEnumerable<UsuarioAdmin>> Listar()
        {
            return _usuariosBD.Listar();
        }

        public Task<UsuarioAdmin> ObtenerPorId(int idUsuario)
        {
            return _usuariosBD.ObtenerPorId(idUsuario);
        }

        public async Task<bool> Crear(RegistrarUsuario usuario)
        {
            Validar(usuario, true);

            usuario.Password =
                _encriptadorAES.Encriptar(usuario.Password);

           return await _usuariosBD.Crear(usuario);
        }


        public Task CambiarEstado(int idUsuario, bool activo)
        {
            return _usuariosBD.CambiarEstado(idUsuario, activo);
        }

        public Task Eliminar(int idUsuario)
        {
            return _usuariosBD.Eliminar(idUsuario);
        }

        private static void Validar(
            RegistrarUsuario usuario,
            bool validarContrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario.Usuario))
                throw new Exception("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                throw new Exception("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new Exception("El correo es obligatorio.");

            if (usuario.IdRol == null || usuario.IdRol <= 0)
                throw new Exception("Debe seleccionar un rol.");

            if (string.IsNullOrWhiteSpace(usuario.Estado))
                throw new Exception("Debe indicar el estado.");

            if (validarContrasena)
            {
                if (string.IsNullOrWhiteSpace(usuario.Password))
                    throw new Exception("La contraseña es obligatoria.");

                if (usuario.Password.Length < 8)
                    throw new Exception("La contraseña debe tener mínimo 8 caracteres.");

                if (!Regex.IsMatch(usuario.Password, @"[A-Z]"))
                    throw new Exception("La contraseña debe tener una mayúscula.");

                if (!Regex.IsMatch(usuario.Password, @"[a-z]"))
                    throw new Exception("La contraseña debe tener una minúscula.");

                if (!Regex.IsMatch(usuario.Password, @"[0-9]"))
                    throw new Exception("La contraseña debe tener un número.");

                if (!Regex.IsMatch(usuario.Password, @"[\W_]"))
                    throw new Exception("La contraseña debe tener un carácter especial.");
            }
        }
    }
}
