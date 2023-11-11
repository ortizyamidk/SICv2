using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class CursoGRepository : RepositoryBase, ICursoGRepository
    {
        //insertar nueva lista de asistencia
        public void AddListaAsistencia(string inicia, string termina, string horario, int duracion, string lugar, int curso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO listacursos (fechainicio, fechaterm, horario, duracion, lugar, idcurso) " +
                                      "VALUES(@inicio, @termina, @horario, @duracion, @lugar, @curso)";

                command.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicia;
                command.Parameters.Add("@termina", SqlDbType.VarChar).Value = termina;
                command.Parameters.Add("@horario", SqlDbType.VarChar).Value = horario;
                command.Parameters.Add("@duracion", SqlDbType.VarChar).Value = duracion;
                command.Parameters.Add("@lugar", SqlDbType.VarChar).Value = lugar;
                command.Parameters.Add("@curso", SqlDbType.Int).Value = curso;

                command.ExecuteNonQuery();
            }
        }

        //insertar instructor dado de alta en bd
        public void AddInstructor(int idinstructor, int idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE cursos SET idinstructor = @idinstructor WHERE id = @idcurso";

                command.Parameters.Add("@idinstructor", SqlDbType.Int).Value = idinstructor;
                command.Parameters.Add("@idcurso", SqlDbType.Int).Value = idcurso;

                command.ExecuteNonQuery();
            }
        }

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
        public CursoGModel GetById(int id)
        {
           CursoGModel asistencia = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerInfoListaAsistencia @idlista";

                command.Parameters.Add("@idlista", SqlDbType.Int).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        asistencia = new CursoGModel()
                        {
                            Id = (int)reader[0],
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),
                            Lugar = reader[3].ToString(),
                            Instructor = reader[4].ToString(),
                            Inicia = reader[5].ToString(),
                            Termina = reader[6].ToString(),
                            Horario = reader[7].ToString(),
                            Duracion = (int)reader[8]                          
                        };
                    }
                }
            }
            return asistencia;
        }

        //ver cursos NO registrados aun de cierta area para mostrarselo a usuario gral en combobox al insertar nueva lista
        public IEnumerable<CursoGModel> GetCursos(string area)
        {
            List<CursoGModel> cursos = new List<CursoGModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT nomcurso FROM cursos WHERE registrado = 0 AND areatematica = @areatematica";

                command.Parameters.Add("@areatematica", SqlDbType.VarChar).Value = area;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoGModel curso = new CursoGModel()
                        {
                            NomCurso = reader[0].ToString()                        
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        //Obtener el id del curso, obteniendolo x su nombre
        public CursoGModel GetIDCursoByNombre(string nombrecurso)
        {
            CursoGModel cursoid = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id, areatematica FROM cursos WHERE nomcurso = @nombrecurso";

                command.Parameters.Add("@nombrecurso", SqlDbType.VarChar).Value = nombrecurso;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cursoid = new CursoGModel()
                        {
                            Id = (int)reader[0],
                            AreaTematica = reader[1].ToString()
                        };
                    }
                }
            }
            return cursoid;
        }

        //se edita el curso de que ya fue registrada su lista de asistencia
        public void IsCursoRegistered(int idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE cursos SET registrado = 1 WHERE id = @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.Int).Value = idcurso;

                command.ExecuteNonQuery();
            }
        }     

        //insertar instructor temporal
        public void AddInstructorTemporal(string nominstr, int idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE listacursos SET nominstr = @nombreinstr WHERE idcurso = @idcurso";

                command.Parameters.Add("@nombreinstr", SqlDbType.VarChar).Value = nominstr;
                command.Parameters.Add("@idcurso", SqlDbType.Int).Value = idcurso;

                command.ExecuteNonQuery();
            }
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
