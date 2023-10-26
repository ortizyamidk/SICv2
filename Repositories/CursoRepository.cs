using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using System.Collections;
using System.Windows;

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
                command.CommandText = "INSERT INTO curso (id, nomcurso, areatematica, fechainicio, fechaterm, horario, duracion, lugar, idinstructor) VALUES(@id, @nomcurso, @area, @inicio, @term, @hor, @dur, @lugar, @idinstr)";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@area", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicia;
                command.Parameters.Add("@term", SqlDbType.VarChar).Value = termina;
                command.Parameters.Add("@hor", SqlDbType.VarChar).Value = horario;
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
                command.CommandText = "INSERT INTO curso (id, nomcurso, areatematica, fechainicio, fechaterm, horario, duracion, lugar, nominstr) VALUES(@id, @nomcurso, @area, @inicio, @term, @hor, @dur, @lugar, @instr)";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@area", SqlDbType.VarChar).Value = areatematica;
                command.Parameters.Add("@inicio", SqlDbType.VarChar).Value = inicia;
                command.Parameters.Add("@term", SqlDbType.VarChar).Value = termina;
                command.Parameters.Add("@hor", SqlDbType.VarChar).Value = horario;
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
                command.CommandText = "UPDATE curso SET nomcurso = @nomcurso, areatematica = @areatem, " +
                                    "fechainicio = @inicia, fechaterm = @termina, horario = @horario, duracion = @duracion, lugar = @lugar, nominstr = NULL, idinstructor = @idinstr " +
                                    "WHERE id = @id";

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
                command.CommandText = "UPDATE curso SET nomcurso = @nomcurso, areatematica = @areatem, " +
                                    "fechainicio = @inicia, fechaterm = @termina, horario = @horario, duracion = @duracion, lugar = @lugar, nominstr = @nominstr, idinstructor = NULL " +
                                    "WHERE id = @id";

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
        public CursoGModel GetAsistenciaById(int id)
        {
            CursoGModel asistencia = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT LC.id, C.nomcurso, C.areatematica, COALESCE(I.nominstr, LC.nominstr) AS nominstr, " +
                                    "CONVERT(varchar,LC.fechainicio, 103) AS fechainicio, CONVERT(varchar,LC.fechaterm, 103) AS fechaterm, " +
                                    "CONVERT(varchar, LC.horario, 108) AS horario, LC.duracion, LC.lugar " +
                                    "FROM cursos as C " +
                                    "LEFT JOIN instructor as I " +
                                    "ON C.idinstructor = I.id " +
                                    "RIGHT JOIN listacursos as LC ON C.id = LC.idcurso " +
                                    "WHERE LC.id = @id";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        asistencia = new CursoGModel()
                        {
                            Id = (int)reader[0],
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString(),
                            Instructor = reader[3].ToString(),
                            Inicia = reader[4].ToString(),
                            Termina = reader[5].ToString(),
                            Horario = reader[6].ToString(),
                            Duracion = (int)reader[7],
                            Lugar = reader[8].ToString()

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
                command.CommandText = "SELECT id, nomcurso, areatematica FROM curso";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CursoModel curso = new CursoModel()
                        {
                            Id = reader[0].ToString(),
                            NomCurso = reader[1].ToString(),
                            AreaTematica = reader[2].ToString()
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
                command.CommandText = "SELECT C.id, C.nomcurso, C.areatematica, " +
                                    "CONVERT(varchar, C.fechainicio, 103) AS fechainicio, " +
                                    "CONVERT(varchar, C.fechaterm, 103) AS fechaterm, " +
                                    "C.horario, C.duracion, C.lugar, COALESCE(C.nominstr, I.nominstr) AS nominstr " +
                                    "FROM curso AS C " +
                                    "LEFT JOIN instructor AS I " +
                                    "ON C.idinstructor = I.id WHERE C.id = @id";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

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

        //ver curso especifico
        public CursoModel GetByName(string nomcurso)
        {
            CursoModel curso = null;
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT C.id, C.nomcurso, C.areatematica, " +
                                        "CONVERT(varchar, C.fechainicio, 103) AS fechainicio, " +
                                        "CONVERT(varchar, C.fechaterm, 103) AS fechaterm, " +
                                        "C.horario, C.duracion, C.lugar, COALESCE(C.nominstr, I.nominstr) AS nominstr " +
                                        "FROM curso AS C " +
                                        "LEFT JOIN instructor AS I " +
                                        "ON C.idinstructor = I.id WHERE C.nomcurso = @nomcurso";

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

        //obtener los cursos que no han sido registrados de cierta area para mostrar en dashboard gral
        public IEnumerable<CursoModel> GetCursosNotRegistered(string area)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT C.nomcurso " +
                    "FROM curso_area AS CA " +
                    "INNER JOIN curso AS C " +
                    "ON CA.idcurso = C.id " +
                    "INNER JOIN area AS A " +
                    "ON CA.idarea = A.id " +
                    "WHERE A.nomarea = @area " +
                    "AND CA.listaregistrada = 0 " +
                    "AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE());";

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

        public IEnumerable<CursoModel> GetCursosVencidos(string area)
        {
            List<CursoModel> cursos = new List<CursoModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT C.nomcurso, DATENAME(MONTH, C.fechaterm) AS MesLimite " +
                    "FROM curso_area AS CA " +
                    "INNER JOIN curso AS C ON CA.idcurso = C.id " +
                    "INNER JOIN area AS A ON CA.idarea = A.id " +
                    "WHERE A.nomarea = @area " +
                    "AND CA.listaregistrada = 0 " +
                    "AND YEAR(C.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C.fechaterm) = YEAR(GETDATE()) " +
                    "AND MONTH(C.fechainicio) < MONTH(GETDATE()) " +
                    "AND MONTH(C.fechaterm) < MONTH(GETDATE()); ";

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

        public CursoModel GetIdByName(string nomcurso)
        {
            CursoModel curso = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM curso WHERE nomcurso = @nomcurso";

                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        curso = new CursoModel()
                        {
                            Id = reader[0].ToString()

                        };
                    }
                }
            }
            return curso;
        }

        public IEnumerable<CursoGModel> GetParticipantes(int id)
        {
            throw new NotImplementedException();
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
                command.CommandText = "SELECT ISNULL(" +
                    "(SELECT COUNT(*) " +
                    "FROM curso AS C " +
                    "INNER JOIN curso_area AS CA ON C.id = CA.idcurso " +
                    "INNER JOIN area AS A ON CA.idarea = A.id " +
                    "WHERE CA.listaregistrada = 1 " +
                    "AND A.nomarea = 'Calidad' " +
                    "AND YEAR(C.fechainicio) = YEAR(GETDATE()) " +
                    "AND YEAR(C.fechaterm) = YEAR(GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, C.fechaterm) = DATEPART(MONTH, GETDATE())), 0) AS CursosRegistrados";


                command.Parameters.Add("@areadpto", SqlDbType.NVarChar).Value = areadpto;

                // Ejecutar la consulta y obtener el recuento
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
                command.CommandText = "SELECT COUNT(nomcurso) AS CursosARegistrarMesActual FROM curso WHERE registrado = 0 " +
                    "AND DATEPART(MONTH, fechainicio) = DATEPART(MONTH, GETDATE()) " +
                    "AND DATEPART(MONTH, fechaterm) = DATEPART(MONTH, GETDATE());";

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

    }
}
