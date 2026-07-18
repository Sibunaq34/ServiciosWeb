using Microsoft.AspNetCore.Http;
using Servicios_Medicos.Repository;
using ServiciosMedicos.Services.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class UbicacionServices : IUbicacion
    {
        private readonly UbicacionRepository _ubicacionBD;
        private readonly BitacoraRepository _bitacora;

        public UbicacionServices(
            UbicacionRepository ubicacionBD,
            BitacoraRepository bitacora)
        {
            _ubicacionBD = ubicacionBD;
            _bitacora = bitacora;
        }

        public async Task CargarUbicaciones(
            Stream archivo,
            string nombreArchivo,
            int idUsuario)
        {
            if (archivo == null)
                throw new Exception("Debe seleccionar un archivo.");

            string extension = Path.GetExtension(nombreArchivo).ToLower();

            if (extension != ".xlsx" && extension != ".xls")
                throw new Exception("Formato de archivo no permitido.");

            var datos = await _ubicacionBD.LeerExcel(archivo);

            await _ubicacionBD.GuardarUbicaciones(datos);

            await _bitacora.Registrar(
                idUsuario,
                "se realizó la carga de información",
                new
                {
                    tabla = "Provincia, Canton, Distrito"
                });
        }

        public Task CargarUbicaciones(IFormFile archivo, int idusuario)
        {
            throw new NotImplementedException();
        }
    }
}