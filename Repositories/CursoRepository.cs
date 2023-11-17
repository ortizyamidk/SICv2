using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class CursoRepository : RepositoryBase, ICursoRepository
    {
        public void AddCursoArea(int idarea, string idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO curso_area (idarea, idcurso) VALUES (@idarea, @idcurso)";

                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;
                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                command.ExecuteNonQuery();
            }
        }

        public void AddCursoInstructor(string id, string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, int idinstructor)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Crear_CursoInstructor @id, @nomcurso, @area, @inicio, @term, @hor, @dur, @lugar, @idinstr";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@area", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicio", SqlDbType.DateTime).Value = inicia;
                command.Parameters.Add("@term", SqlDbType.DateTime).Value = termina;
                command.Parameters.Add("@hor", SqlDbType.DateTime).Value = horario;
                command.Parameters.Add("@dur", SqlDbType.Int).Value = duracion;
                command.Parameters.Add("@lugar", SqlDbType.VarChar).Value = lugar;
                command.Parameters.Add("@idinstr", SqlDbType.Int).Value = idinstructor;

                command.ExecuteNonQuery();
            }
        }

        public void AddCursoInstructorTemporal(string id,string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, string instructor)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Crear_Curso @id, @nomcurso, @area, @inicio, @term, @hor, @dur, @lugar, @instr";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@area", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicio", SqlDbType.DateTime).Value = inicia;
                command.Parameters.Add("@term", SqlDbType.DateTime).Value = termina;
                command.Parameters.Add("@hor", SqlDbType.DateTime).Value = horario;
                command.Parameters.Add("@dur", SqlDbType.Int).Value = duracion;
                command.Parameters.Add("@lugar", SqlDbType.VarChar).Value = lugar;
                command.Parameters.Add("@instr", SqlDbType.VarChar).Value = instructor;

                command.ExecuteNonQuery();
            }
        }

        //editar curso
        public void EditCurso(string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, int idinstructor, string id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Editar_CursoInstructor @nomcurso, @areatem, @inicia, @termina, @horario, @duracion, @lugar, @idinstr, @id ";

                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@areatem", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicia", SqlDbType.VarChar).Value = inicia;
                command.Parameters.Add("@termina", SqlDbType.VarChar).Value = termina;
                command.Parameters.Add("@horario", SqlDbType.VarChar).Value = horario;
                command.Parameters.Add("@duracion", SqlDbType.Int).Value = duracion;
                command.Parameters.Add("@lugar", SqlDbType.VarChar).Value = lugar;
                command.Parameters.Add("@idinstr", SqlDbType.Int).Value = idinstructor;

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                command.ExecuteNonQuery();
            }
        }

        public void EditCursoITemporal(string nomcurso, string areatematica, string inicia, string termina, string horario, int duracion, string lugar, string instructor, string id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Editar_Curso @nomcurso, @areatem, @inicia, @termina, @horario, @duracion, @lugar, @nominstr, @id";

                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@areatem", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicia", SqlDbType.VarChar).Value = inicia;
                command.Parameters.Add("@termina", SqlDbType.VarChar).Value = termina;
                command.Parameters.Add("@horario", SqlDbType.VarChar).Value = horario;
                command.Parameters.Add("@duracion", SqlDbType.Int).Value = duracion;
                command.Parameters.Add("@lugar", SqlDbType.VarChar).Value = lugar;
                command.Parameters.Add("@nominstr", SqlDbType.VarChar).Value = instructor;

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                command.ExecuteNonQuery();
            }
        }

        //ver la lista de asistencia especifica de un curso por el id del curso de todas las areas
        public CursoGModel GetAsistenciaById(string idcurso)
        {
            CursoGModel asistencia = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerAsistenciaGralCursoEsp @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        asistencia = new CursoGModel()
                        {
                            IdCurso = reader[0].ToString(),
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),                           
                            Inicia = reader[3].ToString(),
                            Termina = reader[4].ToString(),
                            Horario = reader[5].ToString(),
                            Duracion = (int)reader[6],
                            Lugar = reader[7].ToString(),
                            Instructor = reader[8].ToString()

                        };
                    }
                }
            }
            return asistencia;
        }
       
        //ver todos los cursos para mostrarlos en tabla de CursosView
        public IEnumerable<CursoModel> GetByAll()
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerCursos";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            Id = reader[0].ToString(),
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),
                            Impartido = reader[3].ToString()
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        public CursoModel GetById(string id)
        {
            CursoModel curso = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerInfoCursoEsp @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        curso = new CursoModel()
                        {
                            Id = reader[0].ToString(),
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),
                            Inicio = reader[3].ToString(),
                            Termino = reader[4].ToString(),
                            Horario = reader[5].ToString(),
                            Duracion = (int)reader[6],
                            Lugar = reader[7].ToString(),
                            Instructor = reader[8].ToString()
                        };
                    }
                }
            }
            return curso;
        }

        //ver curso especifico para registrar listas
        public CursoModel GetByName(string nomcurso)
        {
            CursoModel curso = null;
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "exec VerInfoCursoPorNombre @nomcurso";

                    command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                        curso = new CursoModel()
                            {
                                Id = reader[0].ToString(),
                                NomCurso = reader[1].ToString(),
                                AreaTematica = reader[2].ToString(),
                                Inicio = reader[3].ToString(),
                                Termino = reader[4].ToString(),
                                Horario = reader[5].ToString(),
                                Duracion = (int) reader[6],
                                Lugar = reader[7].ToString(),
                                Instructor = reader[8].ToString()
                            };
                        }
                    }
                }
                return curso;
            
        }

        //reportes
        public CursoModel GetCursoListaAsistencia(string idcurso)
        {
            CursoModel curso = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteAsistenciaInfoCurso @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        curso = new CursoModel()
                        {
                            Id = reader[0].ToString(),
                            NomCurso = reader[1].ToString(),
                            Instructor = reader[2].ToString(),
                            Inicio = reader[3].ToString(),
                            Termino = reader[4].ToString(),
                            Duracion = (int)reader[5],                            
                            Lugar = reader[6].ToString(),
                            Horario = reader[7].ToString(),                           
                        };
                    }
                }
            }
            return curso;
        }

        //reportes
        public IEnumerable<CursoModel> GetCursosHistorialCursos(int numficha)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteHistorialCursos_InfoCurso @numficha";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            NomCurso = reader[0].ToString(),
                            Inicio = reader[1].ToString(),
                            Instructor = reader[2].ToString()
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        //obtener los cursos que no han sido registrados en el mes y año actual de cierta area para mostrar en dashboard gral
        public IEnumerable<CursoModel> GetCursosNotRegistered(string area)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec CursosNoRegistrados_AreaEsp @area";

                command.Parameters.Add("@area", SqlDbType.VarChar).Value = area;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            NomCurso = reader[0].ToString()
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        //cursos vencidos en el año
        public IEnumerable<CursoModel> GetCursosVencidos(string area)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec CursosVencidos_AreaEsp @area";

                command.Parameters.Add("@area", SqlDbType.VarChar).Value = area;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            NomCurso = reader[0].ToString(),
                            MesLimite = reader[1].ToString()
                        };
                        cursos.Add(curso);
                    }
                }
            }
            return cursos;
        }

        //reportes
        public CursoModel GetListaAsistenciaExcel(string idcurso)
        {
            CursoModel curso = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteListaExcel @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        curso = new CursoModel()
                        {                           
                            NomCurso = reader[0].ToString(),
                            Id = reader[1].ToString(),
                            Duracion = (int)reader[2],
                            Inicio = reader[3].ToString(),
                            Termino = reader[4].ToString(),
                            Horario = reader[5].ToString(),
                            Instructor = reader[6].ToString(),
                            idinstructor = (int)reader[7],
                            rfcinstructor = reader[8].ToString(),                                                   
                            Lugar = reader[9].ToString()                           
                        };
                    }
                }
            }
            return curso;
        }

        //obtener la cantidad de cursos q ya fueron registrados para dashboard gral de cierta area
        int ICursoRepository.GetCountCursosRegistered(string areadpto)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec CantidadCursosRegistrados_AreaEsp @areadpto";

                command.Parameters.Add("@areadpto", SqlDbType.NVarChar).Value = areadpto;

                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        //obtener la cantidad del total de cursos que registrar
        int ICursoRepository.GetCountTotalCursos()
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec TotalCursosARegistrar";

                count = (int)command.ExecuteScalar();
            }
            return count;
        }

    }
}
