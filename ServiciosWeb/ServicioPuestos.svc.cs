using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;

namespace ServiciosWeb
{
    public class ServicioPuestos : IServicioPuestos
    {
        private readonly IPuestos _puestosService;

        public ServicioPuestos()
            : this(CrearServicio())
        {
        }

        internal ServicioPuestos(IPuestos puestosService)
        {
            _puestosService = puestosService;
        }

        public List<Puesto> ListarPuestos()
        {
            return _puestosService
                .ListarPuestos()
                .GetAwaiter()
                .GetResult()
                .ToList();
        }

        private static IPuestos CrearServicio()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ConfigurationErrorsException(
                    "No se encontró la cadena de conexión 'DefaultConnection'."
                );
            }

            return new PuestosService(connectionString);
        }
    }
}