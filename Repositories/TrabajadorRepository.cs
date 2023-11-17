using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class TrabajadorRepository : RepositoryBase, ITrabajadorModel
    {
        public void AddTrabajador(int id, string numtarjeta, string nombre, string fechaing, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Crear_Trabajador @id, @numtarjeta, @nombre, @fechaing, @rfc, @escolaridad, @antecedentes, @perscalif, @foto, @auditoriso14001, @idpuesto, @idarea";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@numtarjeta", SqlDbType.VarChar).Value = numtarjeta;
                command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                command.Parameters.Add("@fechaing", SqlDbType.VarChar).Value = fechaing;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@escolaridad", SqlDbType.VarChar).Value = escolaridad;
                command.Parameters.Add("@antecedentes", SqlDbType.VarChar).Value = antecedentes;
                command.Parameters.Add("@perscalif", SqlDbType.VarChar).Value = perscalif;
                command.Parameters.Add("@foto", SqlDbType.VarBinary, foto.Length).Value = foto;
                command.Parameters.Add("@auditoriso14001", SqlDbType.VarChar).Value = auditoriso14001;
                command.Parameters.Add("@idpuesto", SqlDbType.Int).Value = idpuesto;
                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;

                command.ExecuteNonQuery();
            }
        }

        public void EditTrabajador(string numtarjeta, string nombre, string fechaing, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea, int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE trabajador SET  \r\n\t\t\t\t\tnumtarjeta = @numtarjeta, \r\n\t\t\t\t\tnombre = @nombre, \r\n\t\t\t\t\tfechaing = @fechaing, \r\n\t\t\t\t\trfc = @rfc, \r\n\t\t\t\t\tescolaridad = @escolaridad, \r\n\t\t\t\t\tantecedentes = @antecedentes, \r\n\t\t\t\t\tperscalif = @perscalif, \r\n\t\t\t\t\tfoto = @foto, \r\n\t\t\t\t\tauditoriso14001 = @auditoriso14001, \r\n\t\t\t\t\tidpuesto = @idpuesto, \r\n\t\t\t\t\tidarea = @idarea \r\n\r\n\t\t\t\t\tWHERE id = @id";
               
                command.Parameters.Add("@numtarjeta", SqlDbType.VarChar).Value = numtarjeta;
                command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                command.Parameters.Add("@fechaing", SqlDbType.VarChar).Value = fechaing;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@escolaridad", SqlDbType.VarChar).Value = escolaridad;
                command.Parameters.Add("@antecedentes", SqlDbType.VarChar).Value = antecedentes;
                command.Parameters.Add("@perscalif", SqlDbType.VarChar).Value = perscalif;
                command.Parameters.Add("@foto", SqlDbType.VarBinary, foto.Length).Value = foto;
                command.Parameters.Add("@auditoriso14001", SqlDbType.VarChar).Value = auditoriso14001;
                command.Parameters.Add("@idpuesto", SqlDbType.Int).Value = idpuesto;
                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }

        //reportes
        public TrabajadorModel FormatoDC3(int numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteDC3 @numficha";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            Nombre = reader[1].ToString(),
                            RFC = reader[2].ToString(),
                            Puesto = reader[3].ToString()
                        };
                    }
                }
            }
            return trabajador;
        }

        //ver trabajadores para tabla en vista CustomerView
        public IEnumerable<TrabajadorModel> GetByAll()
        {
            List<TrabajadorModel> trabajadores = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec VerTrabajadores";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            NumTarjeta = reader[1].ToString(),
                            Nombre = reader[2].ToString(),
                            Area = reader[3].ToString(),
                            Puesto = reader[4].ToString()
                        };

                        trabajadores.Add(trabajador);
                    }
                }
            }
            return trabajadores;
        }

        //ver trabajador especifico para buscar al crear listas de asistencia
        public TrabajadorModel GetById(int numficha, string nomarea)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec BuscarTrabajadorEsp_PorArea @numficha, @nomarea";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;
                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            Nombre = reader[1].ToString(),
                            Puesto = reader[2].ToString()                           
                        };
                    }
                }
            }
            return trabajador;
        }       

        public TrabajadorModel GetIdByNumTarjeta(string numtarjeta)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM trabajador WHERE numtarjeta = @numtarjeta";

                command.Parameters.Add("@numtarjeta", SqlDbType.VarChar).Value = numtarjeta;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0]
                        };
                    }
                }
            }
            return trabajador;
        }

        public IEnumerable<TrabajadorModel> GetParticipantesById(string idcurso)
        {
            List<TrabajadorModel> participantes = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ParticipantesPorCurso @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel participante = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            NumTarjeta = reader[1].ToString(),
                            Nombre = reader[2].ToString(),
                            Puesto = reader[3].ToString(),
                            Area = reader[4].ToString()
                        };

                        participantes.Add(participante);
                    }
                }
            }
            return participantes;
        }

        public IEnumerable<TrabajadorModel> GetParticipantesByIdAndArea(string idcurso, string area)
        {
            List<TrabajadorModel> participantes = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ParticipantesPorCurso_PorArea @idcurso, @area";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;
                command.Parameters.Add("@area", SqlDbType.VarChar).Value = area;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel participante = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            NumTarjeta = reader[1].ToString(),
                            Nombre = reader[2].ToString(),
                            Puesto = reader[3].ToString(),
                            Area = reader[4].ToString()
                        };

                        participantes.Add(participante);
                    }
                }
            }
            return participantes;
        }

        //participantes para CursoInfoGView
        public IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista, string nomarea)
        {
            List<TrabajadorModel> participantes = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ParticipantesPorListaAsistencia @idlista, @nomarea";

                command.Parameters.Add("@idlista", SqlDbType.Int).Value = idlista;
                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel participante = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            NumTarjeta = reader[1].ToString(),
                            Nombre = reader[2].ToString(),
                            Puesto = reader[3].ToString()
                        };

                        participantes.Add(participante);
                    }
                }
            }
            return participantes;
        }

        //reportes
        public IEnumerable<TrabajadorModel> GetPersonalCalificadoByArea(string nomarea)
        {
            List<TrabajadorModel> personasCalif = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReportePersonalCalificado_PorArea @nomarea";

                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel personalCalif = new TrabajadorModel()
                        {
                            Area = reader[0].ToString(),
                            Id = (int)reader[1],
                            Nombre = reader[2].ToString(),                                                                                
                            Puesto = reader[3].ToString()
                        };

                        personasCalif.Add(personalCalif);
                    }
                }
            }
            return personasCalif;
        }

        //reportes
        public IEnumerable<TrabajadorModel> GetPersonalCalificadoGral()
        {
            List<TrabajadorModel> personasCalif = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReportePersonalCalificadoGral";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel personalCalif = new TrabajadorModel()
                        {
                            Area = reader[0].ToString(),
                            Id = (int)reader[1],
                            Nombre = reader[2].ToString(),                         
                            Puesto = reader[3].ToString()
                        };

                        personasCalif.Add(personalCalif);
                    }
                }
            }
            return personasCalif;
        }

        public TrabajadorModel GetTrabajador(int numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT T.id, T.numtarjeta, CONVERT(varchar, T.fechaing, 103) as fechaing, T.nombre, T.rfc, COALESCE(T.antecedentes, '') AS antecedentes, DATEDIFF(YEAR, T.fechaing, GETDATE()) AS antiguedad_anios, DATEDIFF(MONTH, T.fechaing, GETDATE()) % 12 AS antiguedad_meses, P.categoria\r\nFROM trabajador AS T\r\nINNER JOIN puesto AS P\r\nON T.idpuesto = P.id\r\nWHERE T.id = @numficha";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            NumTarjeta = reader[1].ToString(),
                            FechaIngreso = reader[2].ToString(),
                            Nombre = reader[3].ToString(),
                            RFC = reader[4].ToString(),
                            Antecedentes = reader[5].ToString(),
                            antiguedadanios = (int)reader[6],
                            antiguedadmeses = (int)reader[7],
                            Categoria = reader[8].ToString()
                        };
                    }
                }
            }
            return trabajador;
        }

        //reportes
        public IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistencia(string idcurso)
        {
            List<TrabajadorModel> trabajadores = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Reporte_AsistenciaACurso_Trabajadores @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            Nombre = reader[1].ToString(),
                            Area = reader[2].ToString()
                        };

                        trabajadores.Add(trabajador);
                    }
                }
            }
            return trabajadores;
        }

        public IEnumerable<TrabajadorModel> GetTrabajadoresListaAsistenciaExcel(string idcurso)
        {
            List<TrabajadorModel> trabajadores = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteExcel_Trabajadores @idcurso";

                command.Parameters.Add("@idcurso", SqlDbType.VarChar).Value = idcurso;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TrabajadorModel trabajador = new TrabajadorModel()
                        {
                            Nombre = reader[0].ToString(),
                            Id = (int)reader[1],                           
                            Area = reader[2].ToString()
                        };

                        trabajadores.Add(trabajador);
                    }
                }
            }
            return trabajadores;
        }

        //reportes
        public TrabajadorModel GetTrabajadorHistorialCursos(int numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteHistorialCursos_TrabajadorEsp @numficha";

                command.Parameters.Add("@numficha", SqlDbType.Int).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = (int)reader[0],
                            Nombre = reader[1].ToString(),
                            Puesto = reader[2].ToString(),
                            Departamento = reader[3].ToString(),
                            Area = reader[4].ToString(),
                            FechaIngreso = reader[5].ToString()
                        };
                    }
                }
            }
            return trabajador;
        }
    }
}
