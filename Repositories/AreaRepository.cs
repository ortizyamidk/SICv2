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
    public class AreaRepository : RepositoryBase, IAreaRepository
    {
        public IEnumerable<AreaModel> GetIdAreasRegistran()
        {
            List<AreaModel> areas = new List<AreaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM area WHERE registracursos = 1";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AreaModel area = new AreaModel()
                        {
                            Id = (int)reader[0]
                        };
                        areas.Add(area);
                    }
                }
            }
            return areas;
        }

        public AreaModel GetIdByName(string nomarea)
        {
            AreaModel area = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM area WHERE nomarea = @nomarea";

                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        area = new AreaModel()
                        {
                            Id = (int)reader[0]

                        };
                    }
                }
            }
            return area;
        }
    }
}
