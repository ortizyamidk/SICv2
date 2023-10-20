using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class DepartamentoRepository : RepositoryBase, IDepartamentoRepository
    {
        //ver los departamentos que SÍ registran cursos (modificar) para dashboard Principal
        public IEnumerable<DepartamentoModel> GetDepartamentos()
        {
            List<DepartamentoModel> deptos = new List<DepartamentoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT A.nomarea FROM area AS A INNER JOIN departamento AS D ON A.iddpto = D.id WHERE D.registra = 1";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DepartamentoModel depto = new DepartamentoModel()
                        {
                            NomDepto = reader[0].ToString(),
                        };
                        deptos.Add(depto);
                    }
                }
            }
            return deptos;
        }
    }
}
