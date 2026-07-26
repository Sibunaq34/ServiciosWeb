using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class PuestosService : IPuestos
    {
        private readonly IPuestosRepository _puestosRepository;

        public PuestosService(string connectionString)
        {
            IDbConnectionFactory factory = new DbConnectionFactory(connectionString);
            _puestosRepository = new PuestosRepository(factory);
        }

        public Task<IEnumerable<Puesto>> ListarPuestos()
        {
            return _puestosRepository.ListarPuestos();
        }

        public Task<Puesto> ObtenerPuesto(int idPuesto)
        {
            return _puestosRepository.ObtenerPuesto(idPuesto);
        }

        public Task<bool> InsertarPuesto(Puesto puesto)
        {
            return _puestosRepository.InsertarPuesto(puesto);
        }

        public Task<bool> ActualizarPuesto(Puesto puesto)
        {
            return _puestosRepository.ActualizarPuesto(puesto);
        }

        public Task<bool> EliminarPuesto(int idPuesto)
        {
            return _puestosRepository.EliminarPuesto(idPuesto);
        }
    }
}