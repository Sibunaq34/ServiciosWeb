
using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System;
using System.Threading.Tasks;


namespace Servicios_Medicos.Services
{
    public class AutenticacionServices : IUsuario
    {
        private readonly SeguridadRepository _seguridadBD;
        private readonly EncriptadorAESServices _aes;

        public AutenticacionServices(
            SeguridadRepository seguridadBD,
            EncriptadorAESServices aes)
        {
            _seguridadBD = seguridadBD;
            _aes = aes;
        }


        public async Task<SeguridadLog> Login(string usuario, string password)
        {
            var entidad = await _seguridadBD.ObtenerUsuario(usuario);

            if (entidad == null)
                return null;

            if (entidad.Estado == "Inactivo")
            {
                throw new Exception(
                    "Usuario y/o \r\ncontraseña incorrectos.");
            }

            bool valido =
                _aes.CompararPassword(password, entidad.PasswordCifrada);

            if (!valido)
            {
                int intentos = entidad.intentos_fallidos + 1;

                await _seguridadBD.RegistrarIntentoFallido(entidad.IdUsuario, intentos);

                if (intentos >= 3)
                {
                    throw new Exception("Usuario y/o \r\ncontraseña incorrectos.");
                }

                throw new Exception(
                    $"Usuario y/o \r\ncontraseña incorrectos.");
            }


            await _seguridadBD.RegistrarIntentoFallido(entidad.IdUsuario, 0);

            return entidad;

        }
    }
}