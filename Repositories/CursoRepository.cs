using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using System.Collections;

namespace WPF_LoginForm.Repositories
{
    public class CursoRepository : RepositoryBase, ICursoRepository
    {
        //insertar curso nuevo
        public void AddCurso(string nomcurso, string areatem, string meslim)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO cursos (nomcurso, areatematica, meslim) VALUES (@nomcurso, @areatem, @meslim)";

                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@areatem", SqlDbType.VarChar).Value = areatem;
                command.Parameters.Add("@meslim", SqlDbType.VarChar).Value = meslim;

                command.ExecuteNonQuery();
            }
        }

        //editar curso
        public void EditCurso(string nomcurso, string areatem, string meslim, int idcurso)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE cursos SET nomcurso = @nomcurso, areatematica = @areatem, meslim = @meslim WHERE id = @id";

                command.Parameters.Add("@nomcurso", SqlDbType.VarChar).Value = nomcurso;
                command.Parameters.Add("@areatem", SqlDbType.VarChar).Value = areatem;
                command.Parameters.Add("@meslim", SqlDbType.VarChar).Value = meslim;

                command.Parameters.Add("@id", SqlDbType.Int).Value = idcurso;

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

        //ver curso especifico
        public CursoModel GetById(int id)
        {
            CursoModel curso = null;
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT id, nomcurso, areatematica, meslim FROM cursos WHERE id = @id";

                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                        curso = new CursoModel()
                            {
                                Id = (int)reader[0],
                                NomCurso = reader[1].ToString(),
                                AreaTematica = reader[2].ToString(),
                                MesLimite = reader[3].ToString()

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
                command.CommandText = "SELECT nomcurso FROM cursos WHERE areatematica=@area AND registrado=0;";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;
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

        public IEnumerable<CursoGModel> GetParticipantes(int id)
        {
            throw new NotImplementedException();
        }

        //obtener la cantidad de cursos q ya fueron registrados para dashboard gral de cierta area
        int ICursoRepository.GetCountCursosRegistered(string area)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM cursos WHERE areatematica=@area AND registrado=1;";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

        //obtener la cantidad del total de cursos que hay de cierta area
        int ICursoRepository.GetCountTotalCursos(string area)
        {
            int count = 0;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM cursos WHERE areatematica=@area";
                command.Parameters.Add("@area", SqlDbType.NVarChar).Value = area;

                // Ejecutar la consulta y obtener el recuento
                count = (int)command.ExecuteScalar();
            }
            return count;
        }

    }
}
