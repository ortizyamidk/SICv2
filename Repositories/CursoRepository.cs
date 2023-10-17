using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class CursoRepository : RepositoryBase, ICursoRepository
    {
        public IEnumerable<CursoModel> GetByAll()
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select id, nomcurso, areatematica from cursos";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            Id = (int)reader[0],
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        public IEnumerable<CursoModel> GetCursosNotRegistered(string area)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select nomcurso from cursos where areatematica=@area AND registrado=0;";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            NomCurso = reader[0].ToString(),
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        int ICursoRepository.GetCountCursosRegistered(string area)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select count(*) from cursos where areatematica=@area AND registrado=1;";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        int ICursoRepository.GetCountTotalCursos(string area)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select count(*) from cursos where areatematica=@area";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

    }
}
