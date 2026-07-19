using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using Servicios_Medicos.Repository;
using Servicios_Medicos.Services;
using ServiciosMedicos.Entities;

namespace ServiciosWeb
{
    public class ServicioOferentesPuesto : IServicioOferentesPuesto
    {
        private readonly OferentesPorPuestoService _service;

        public ServicioOferentesPuesto()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var factory = new DbConnectionFactory(connectionString);
            var oferentesRepository = new OferentesPorPuestoRepository(factory);
            var requisitosRepository = new RequisitosPorPuestoRepository(factory);
            _service = new OferentesPorPuestoService(oferentesRepository, requisitosRepository);
        }

        public List<RequisitoPuestoDto> ListarRequisitosPorPuesto(string codigoPuesto)
        {
            try
            {
                var resultados = _service.ListarRequisitosPorPuesto(codigoPuesto)
                    .GetAwaiter()
                    .GetResult()
                    .Where(x => !string.IsNullOrWhiteSpace(x.NombreRequisito))
                    .OrderBy(x => x.NombreRequisito)
                    .ToList();

                return resultados ?? new List<RequisitoPuestoDto>();
            }
            catch
            {
                throw new FaultException("No fue posible consultar la información del puesto.");
            }
        }

        public List<OferenteCumplimientoDto> ListarOferentesPorPuesto(string codigoPuesto)
        {
            try
            {
                var resultados = _service.ListarOferentesPorPuesto(codigoPuesto)
                    .GetAwaiter()
                    .GetResult()
                    .Where(x => !string.IsNullOrWhiteSpace(x.NombreCompleto) && !string.IsNullOrWhiteSpace(x.Identificacion))
                    .OrderBy(x => x.NombreCompleto)
                    .ToList();

                return resultados ?? new List<OferenteCumplimientoDto>();
            }
            catch
            {
                throw new FaultException("No fue posible consultar la información del puesto.");
            }
        }
    }
}
