using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class CursoGRepository : RepositoryBase, ICursoGRepository
    {
        //insertar participantes de la lista de asistencia a un curso
        public void AddParticipantes(int numficha, string idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO cursotrabajador (idtrabajador, idcurso) VALUES (@numficha, @idcurso)";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;
                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                command.ExecuteNonQuery();
            }
        }

        //pase de lista
        public void Edit(string idcurso, int numficha)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE cursotrabajador SET asistio = 1 WHERE idcurso = @idcurso AND idtrabajador = @numficha";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;
                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;

                command.ExecuteNonQuery();
            }
        }

        //ver todos los cursos de cierta area que ya está registrada su lista de asistencia para tabla
        public IEnumerable<CursoGModel> GetByAll(string nomarea)
        {
            List<CursoGModel> cursos = new List<CursoGModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec CursosRegistradosPorArea @nomarea";

                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoGModel curso = new CursoGModel()
                        {
                            IdLista = (int)reader[0],
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),                           
                            Inicia = ((DateTime)reader[3]).ToString("dd/MM/yyyy"),
                            Termina = ((DateTime)reader[4]).ToString("dd/MM/yyyy"),
                            Instructor = reader[5].ToString()
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        //ver la lista de asistencia especifica al dar click en ver info de tabla
        public CursoGModel GetById(int idlista, string nomarea)
        {
           CursoGModel asistencia = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerInfoListaAsistencia @idlista, @nomarea";

                command.Parameters.Add("@idlista", SqlDbType.Int).Value = idlista;
                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        asistencia = new CursoGModel()
                        {
                            IdLista = (int)reader[0],
                            IdCurso = reader[1].ToString(),
                            NomCurso = reader[2].ToString(),
                            AreaTematica = reader[3].ToString(),
                            Lugar = reader[4].ToString(),
                            Instructor = reader[5].ToString(),
                            Inicia = reader[6].ToString(),
                            Termina = reader[7].ToString(),
                            Horario = reader[8].ToString(),
                            Duracion = (int)reader[9]                          
                        };
                    }
                }
            }
            return asistencia;
        } 

        public void AddListaAsistencia(int idarea, string idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE curso_area SET listaregistrada = 1 WHERE idcurso = @idcurso AND idarea = @idarea";

                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;
                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                command.ExecuteNonQuery();
            }
        }

        public int CursoImpartido(string nomcurso)
        {
            int cursoImpartido = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec IsCursoImpartido @nomcurso";

                command.Parameters.Add("@nomcurso", SqlDbType.NVarChar).Value = nomcurso;

                cursoImpartido = (int)command.ExecuteScalar();
            }
            return cursoImpartido;
        }
    }
}
