using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class AreaRepository : RepositoryBase, IAreaRepository
    {
        public IEnumerable<AreaModel> GetAreaByDepartamento(string depto)
        {
            List<AreaModel> areas = new List<AreaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT A.nomarea FROM departamento AS D INNER JOIN area AS A ON D.id = A.iddpto WHERE D.nomdepto = @nomdepto";
                
                command.Parameters.Add("@nomdepto", SqlDbType.VarChar).Value = depto;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AreaModel area = new AreaModel()
                        {
                            NombreArea = reader[0].ToString()
                        };
                        areas.Add(area);
                    }
                }
            }
            return areas;
        }

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

        public IEnumerable<AreaModel> GetByAll()
        {
            List<AreaModel> areas = new List<AreaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT nomarea FROM area";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AreaModel area = new AreaModel()
                        {
                            NombreArea = reader[0].ToString()
                        };
                        areas.Add(area);
                    }
                }
            }
            return areas;
        }

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

        public IEnumerable<AreaModel> GetIdsAreasByName(string nomarea)
        {
            List<AreaModel> areas = new List<AreaModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM area WHERE nomarea = @nomarea";

                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

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

        public IEnumerable<AreaModel> GetProgresoAreas()
        {
            List<AreaModel> areas = new List<AreaModel>();
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
                        AreaModel area = new AreaModel()
                        {
                            NombreArea = reader[0].ToString(),
                            CursosRegistrados = (int)reader[1],
                            CursosARegistrar = (int)reader[2],
                            PorcentajeAvance = (int)reader[3],
                            ValorPorcentaje = (int)reader[4]
                        };
                        areas.Add(area);
                    }
                }
            }
            return areas;
        }

        public int GetTotalAreas()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec TotalAreasDebenReg";

                count = (int)command.ExecuteScalar();
            }
            return count;
        }
    }
}
