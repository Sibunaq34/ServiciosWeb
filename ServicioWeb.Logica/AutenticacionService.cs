using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class AutenticacionServices : IUsuario
    {
        private const string MensajeGenerico = "Usuario y/o contraseña incorrectos.";

        private readonly SeguridadRepository _seguridadBD;
        private readonly EncriptadorAESServices _aes;

        public AutenticacionServices(
            SeguridadRepository seguridadBD,
            EncriptadorAESServices aes)
        {
            _seguridadBD = seguridadBD;
            _aes = aes;
        }

        public async Task<ResultadoAutenticacion> Login(string usuario, string password)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                return new ResultadoAutenticacion
                {
                    Exito = false,
                    Mensaje = MensajeGenerico
                };
            }

            var usuarioNormalizado = usuario.Trim();
            var entidad = await _seguridadBD.ObtenerUsuario(usuarioNormalizado);

            if (entidad == null)
            {
                return new ResultadoAutenticacion
                {
                    Exito = false,
                    Mensaje = MensajeGenerico
                };
            }

            if (!entidad.Activo || string.Equals(entidad.Estado, "Inactivo", StringComparison.OrdinalIgnoreCase) || string.Equals(entidad.Estado, "Bloqueado", StringComparison.OrdinalIgnoreCase))
            {
                return new ResultadoAutenticacion
                {
                    Exito = false,
                    Mensaje = MensajeGenerico
                };
            }

            var valido = _aes.CompararPassword(password, entidad.PasswordCifrada);

            if (!valido)
            {
                var intentos = entidad.IntentosFallidos + 1;
                await _seguridadBD.RegistrarIntentoFallido(entidad.IdUsuario, intentos);

                return new ResultadoAutenticacion
                {
                    Exito = false,
                    Mensaje = MensajeGenerico
                };
            }

            await _seguridadBD.ReiniciarIntentosFallidos(entidad.IdUsuario);

            if (!string.IsNullOrWhiteSpace(entidad.PasswordCifrada) && !_aes.EsFormatoGcm(entidad.PasswordCifrada))
            {
                var passwordCifrada = _aes.Encriptar(password);
                await _seguridadBD.ActualizarPasswordCifradaUsuario(entidad.IdUsuario, passwordCifrada);
            }

            return new ResultadoAutenticacion
            {
                Exito = true,
                Mensaje = "Autenticación correcta.",
                IdUsuario = entidad.IdUsuario,
                Usuario = entidad.Usuario,
                NombreCompleto = entidad.NombreCompleto,
                IdRol = entidad.IdRol,
                NombreRol = entidad.NombreRol,
                Estado = entidad.Estado
            };
        }
    }
}