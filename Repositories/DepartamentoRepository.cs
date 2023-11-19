using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class DepartamentoRepository : RepositoryBase, IDepartamentoRepository
    {
        public IEnumerable<DepartamentoModel> GetByAll()
        {
            List<DepartamentoModel> deptos = new List<DepartamentoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT nomdepto FROM departamento";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartamentoModel depto = new DepartamentoModel()
                        {
                            NomDepto = reader[0].ToString()
                        };
                        deptos.Add(depto);
                    }
                }
            }
            return deptos;
        }

        public DepartamentoModel GetJefeByDepartamento(string depto)
        {
            DepartamentoModel jefe = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT jefedepto FROM departamento WHERE nomdepto = @depto";

                command.Parameters.Add("@depto", SqlDbType.VarChar).Value = depto;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        jefe = new DepartamentoModel()
                        {
                            Jefe = reader[0].ToString()
                        };
                    }
                }
            }
            return jefe;
        }
    }
}
