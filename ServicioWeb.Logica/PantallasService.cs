using ServiciosMedicos.Entities;
using Servicios_Medicos.Repository;
using ServiciosMedicos.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{

    public class PantallasServices : IPantallas
    {
        private readonly PantallasRepository _pantallasBD;

        public PantallasServices(PantallasRepository pantallasBD)
        {
            _pantallasBD = pantallasBD;
        }

        public Task<IEnumerable<Pantalla>> Listar()
        {
            return _pantallasBD.Listar();
        }

        public Task<Pantalla> ObtenerPorId(int idPantalla)
        {
            return _pantallasBD.ObtenerPorId(idPantalla);
        }

        public async Task Crear(Pantalla pantalla)
        {
            Validar(pantalla);
            await _pantallasBD.Crear(pantalla);
        }

        public async Task Actualizar(Pantalla pantalla)
        {
            Validar(pantalla);
            await _pantallasBD.Actualizar(pantalla);
        }

        public Task Eliminar(int idPantalla)
        {
            return _pantallasBD.Eliminar(idPantalla);
        }

        private static void Validar(Pantalla pantalla)
        {
            if (string.IsNullOrWhiteSpace(pantalla.NombrePantalla))
                throw new Exception("El nombre de la pantalla es obligatorio.");

            if (pantalla.NombrePantalla.Length > 100)
                throw new Exception("El nombre de la pantalla no debe superar 100 caracteres.");

            if (!Regex.IsMatch(pantalla.NombrePantalla, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new Exception("El nombre de la pantalla solo debe tener letras y espacios.");
        }
    }
}