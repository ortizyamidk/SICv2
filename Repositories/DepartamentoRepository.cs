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
        //obtener la cantidad de areas que ya terminaron de registrar todos los cursos
        public int GetAreasTerminadas()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec AreasListas";

                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        //DASHBOARD PRINCIPAL ver departamentos y su avance de progreso en registro de cursos
        public IEnumerable<DepartamentoModel> GetDepartamentos()
        {
            List<DepartamentoModel> deptos = new List<DepartamentoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ProgresoAreas";

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

        //Contar el total de áreas que registran cursos para grafico de progreso
        public int GetTotalAreas()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(nomarea) AS AreasQueRegistran FROM area WHERE registracursos = 1";

                count = (int)command.ExecuteScalar();
            }
            return count;
        }
    }
}
