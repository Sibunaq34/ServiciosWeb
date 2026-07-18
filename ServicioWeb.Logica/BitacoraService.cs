using Servicios_Medicos.Repository;
using ServiciosMedicos.Entities;
using ServiciosMedicos.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios_Medicos.Services
{
    public class BitacoraServices : IBitacora
    {
        private readonly BitacoraConsultaRepository _bitacoraBD;

        public BitacoraServices(BitacoraConsultaRepository bitacoraBD)
        {
            _bitacoraBD = bitacoraBD;
        }

        public async Task<IReadOnlyList<Bitacora>> ConsultarBitacoras(
            string usuario,
            string descripcion,
            int pagina,
            int tamanoPagina)
        {
            var resultado =
                await _bitacoraBD.ConsultarBitacoras(
                    usuario,
                    descripcion,
                    pagina,
                    tamanoPagina);


            return resultado.ToList();
        }
    }
}