using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    public class ServicioPuestos : IServicioPuestos
    {
        private readonly PuestosService _puestosService;

        public ServicioPuestos()
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
                new PuestosRepository(factory);

            _puestosService =
                new PuestosService(repository);
        }

        public List<Puesto> ListarPuestosActivos()
        {
            return _puestosService
                .ListarPuestos()
                .GetAwaiter()
                .GetResult()
                .ToList();
        }
    }
}