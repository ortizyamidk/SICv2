using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class PuestoRepository : RepositoryBase, IPuestoRepository
    {
        public IEnumerable<PuestoModel> GetByAll()
        {
            List<PuestoModel> puestos = new List<PuestoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT nompuesto FROM puesto";


                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PuestoModel puesto = new PuestoModel()
                        {
                            NomPuesto = reader[0].ToString()
                        };
                        puestos.Add(puesto);
                    }
                }
            }
            return puestos;
        }

        public PuestoModel GetCategoriaByPuesto(string nompuesto)
        {
            PuestoModel puesto = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT categoria FROM puesto WHERE nompuesto = @nompuesto";

                command.Parameters.Add("@nompuesto", SqlDbType.VarChar).Value = nompuesto;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        puesto = new PuestoModel()
                        {
                            Categoria = reader[0].ToString()

                        };
                    }
                }
            }
            return puesto;
        }

        public PuestoModel GetIdByNombrePuesto(string nompuesto)
        {
            PuestoModel puesto = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM puesto WHERE nompuesto = @nompuesto";

                command.Parameters.Add("@nompuesto", SqlDbType.VarChar).Value = nompuesto;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        puesto = new PuestoModel()
                        {
                            Id = (int)reader[0]

                        };
                    }
                }
            }
            return puesto;
        }
    }
}
