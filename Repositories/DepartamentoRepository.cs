using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class DepartamentoRepository : RepositoryBase, IDepartamentoRepository
    {
        public int GetAreasTerminadas()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) AS RegistrosConPorcentaje100\r\nFROM (\r\n    SELECT\r\n        A.nomarea AS AREA,\r\n        (\r\n            (\r\n                SELECT ISNULL(COUNT(*), 0)\r\n                FROM curso AS C\r\n                INNER JOIN curso_area AS CA ON C.id = CA.idcurso\r\n                INNER JOIN area AS A2 ON CA.idarea = A2.id\r\n                WHERE CA.listaregistrada = 1\r\n                AND A2.nomarea = A.nomarea\r\n                AND YEAR(C.fechainicio) = YEAR(GETDATE()) \r\n                AND YEAR(C.fechaterm) = YEAR(GETDATE())\r\n                AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE())\r\n                AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE())\r\n            ) * 100.0\r\n        ) / \r\n        (\r\n            SELECT COUNT(nomcurso)\r\n            FROM curso AS C2\r\n            WHERE C2.registrado = 0\r\n            AND DATEPART(MONTH, C2.fechainicio) = DATEPART(MONTH, GETDATE())\r\n            AND DATEPART(MONTH, C2.fechaterm) = DATEPART(MONTH, GETDATE())\r\n            AND YEAR(C2.fechainicio) = YEAR(GETDATE()) \r\n            AND YEAR(C2.fechaterm) = YEAR(GETDATE())\r\n        ) AS PorcentajeAvance\r\n    FROM area AS A\r\n    WHERE A.registracursos = 1\r\n) AS Subconsulta\r\nWHERE Subconsulta.PorcentajeAvance = 100";

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        //ver los departamentos que SÍ registran cursos (modificar) para dashboard Principal
        public IEnumerable<DepartamentoModel> GetDepartamentos()
        {
            List<DepartamentoModel> deptos = new List<DepartamentoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT " +
                    "A.nomarea AS AREA, " +
                    "(SELECT ISNULL(COUNT(*), 0) " +
                    "FROM curso AS C " +
                    "INNER JOIN curso_area AS CA ON C.id = CA.idcurso " +
                    "INNER JOIN area AS A2 ON CA.idarea = A2.id " +
                    "WHERE CA.listaregistrada = 1 " +
                    "AND A2.nomarea = A.nomarea " +
                    "AND YEAR(C.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C.fechaterm) = YEAR(GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE())) AS CursosRegistrados, " +
                    "(SELECT COUNT(nomcurso) " +
                    "FROM curso AS C2 " +
                    "WHERE C2.registrado = 0 " +
                    "AND DATEPART(MONTH, C2.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C2.fechaterm) = DATEPART(MONTH, GETDATE()) " +
                    "AND YEAR(C2.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C2.fechaterm) = YEAR(GETDATE())) AS CursosARegistrarMesActual, " +
                    "CAST(((SELECT ISNULL(COUNT(*), 0) FROM curso AS C " +
                    "INNER JOIN curso_area AS CA ON C.id = CA.idcurso " +
                    "INNER JOIN area AS A2 ON CA.idarea = A2.id " +
                    "WHERE CA.listaregistrada = 1 " +
                    "AND A2.nomarea = A.nomarea " +
                    "AND YEAR(C.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C.fechaterm) = YEAR(GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE())  " +
                    "AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE())) * 100.0) / (SELECT COUNT(nomcurso) FROM curso AS C2 WHERE C2.registrado = 0 " +
                    "AND DATEPART(MONTH, C2.fechainicio) = DATEPART(MONTH, GETDATE()) AND DATEPART(MONTH, C2.fechaterm) = DATEPART(MONTH, GETDATE())  AND YEAR(C2.fechainicio) = YEAR(GETDATE()) AND YEAR(C2.fechaterm) = YEAR(GETDATE())) AS INT) AS PorcentajeAvance, CAST((CAST(((" +
                    "SELECT ISNULL(COUNT(*), 0) FROM curso AS C INNER JOIN curso_area AS CA ON C.id = CA.idcurso INNER JOIN area AS A2 ON CA.idarea = A2.id WHERE CA.listaregistrada = 1 AND A2.nomarea = A.nomarea AND YEAR(C.fechainicio) = YEAR(GETDATE()) AND YEAR(C.fechaterm) = YEAR(GETDATE()) AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE())) * 100.0) / (SELECT COUNT(nomcurso) FROM curso AS C2 WHERE C2.registrado = 0 " +
                    "AND DATEPART(MONTH, C2.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C2.fechaterm) = DATEPART(MONTH, GETDATE()) AND YEAR(C2.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C2.fechaterm) = YEAR(GETDATE())) AS INT) / 10) AS INT) AS ValuePorcentaje " +
                    "FROM area AS A " +
                    "WHERE A.registracursos = 1";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartamentoModel depto = new DepartamentoModel()
                        {
                            NomDepto = reader[0].ToString(),
                            CursosRegistrados = (int)reader[1],
                            CursosARegistrar = (int)reader[2],
                            PorcentajeAvance = (int)reader[3],
                            ValorPorcentaje = (int)reader[4]
                        };
                        deptos.Add(depto);
                    }
                }
            }
            return deptos;
        }

        public int GetTotalAreas()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(nomarea) AS AreasQueRegistran FROM area WHERE registracursos = 1";

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }
    }
}
