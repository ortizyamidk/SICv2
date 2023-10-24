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
                command.CommandText = "SELECT COUNT(*) AS AreasTerminadas " +
                    "FROM (SELECT A.nomarea, SUM(CASE WHEN CA.listaregistrada = 1 THEN 1 ELSE 0 END) AS count_registrada, COUNT(*) AS total_cursos, " +
                    "(SUM(CASE WHEN CA.listaregistrada = 1 THEN 1 ELSE 0 END) * 100) / COUNT(*) AS porcentaje " +
                    "FROM area AS A " +
                    "INNER JOIN curso_area AS CA " +
                    "ON A.id = CA.idarea " +
                    "GROUP BY A.nomarea " +
                    "HAVING COUNT(*) = (SELECT COUNT(DISTINCT idcurso) FROM curso_area)) AS subquery " +
                    "WHERE subquery.porcentaje = 100;";

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
                command.CommandText = "SELECT A.nomarea, SUM(CASE WHEN CA.listaregistrada = 1 THEN 1 ELSE 0 END) AS count_registrada, " +
                    "COUNT(*) AS total_cursos, (SUM(CASE WHEN CA.listaregistrada = 1 THEN 1 ELSE 0 END) * 100) / COUNT(*) AS porcentaje, " +
                    "((SUM(CASE WHEN CA.listaregistrada = 1 THEN 1 ELSE 0 END) * 100) / COUNT(*)) / 10 AS ValuePorcentaje " +
                    "FROM area AS A " +
                    "INNER JOIN curso_area AS CA " +
                    "ON A.id = CA.idarea " +
                    "GROUP BY A.nomarea " +
                    "HAVING COUNT(*) = (SELECT COUNT(DISTINCT idcurso) FROM curso_area)";

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
