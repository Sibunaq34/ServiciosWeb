using ClosedXML.Excel;
using Dapper;
using ServiciosMedicos.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Servicios_Medicos.Repository
{
    public class UbicacionRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UbicacionRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<Ubicacion>> LeerExcel(
            Stream archivo)
        {
            var ubicaciones =
                new List<Ubicacion>();

            using (var workbook = new XLWorkbook(archivo))
            {
                var hoja = workbook.Worksheet(1);

                var filas = hoja.RowsUsed().Skip(1);

                foreach (var fila in filas)
                {
                    ubicaciones.Add(
                        new Ubicacion
                        {
                            Provincia = fila.Cell(1).GetString(),
                            Canton = fila.Cell(2).GetString(),
                            Distrito = fila.Cell(3).GetString()
                        });
                }
            }

            return ubicaciones;
        }

        public async Task GuardarUbicaciones(
            List<Ubicacion> ubicaciones)
        {
            Console.WriteLine($"Registros encontrados: {ubicaciones.Count}");

            using (var connection = _connectionFactory.CreateConnection())
            {
                foreach (var item in ubicaciones)
                {
                    await connection.ExecuteAsync(
                        "GuardarUbicacion",
                        new
                        {
                            pProvincia = item.Provincia,
                            pCanton = item.Canton,
                            pDistrito = item.Distrito
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}