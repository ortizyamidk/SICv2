using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class TrabajadorRepository : RepositoryBase, ITrabajadorRepository
    {
        public void AddTrabajador(string id, string numtarjeta, string nombre, DateTime fechaing, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea, byte[] certificaciones)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Crear_Trabajador @id, @numtarjeta, @nombre, @fechaing, @rfc, @escolaridad, @antecedentes, @perscalif, @foto, @auditoriso14001, @idpuesto, @idarea, @certificaciones";

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                command.Parameters.Add("@numtarjeta", SqlDbType.VarChar).Value = numtarjeta;
                command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                command.Parameters.Add("@fechaing", SqlDbType.DateTime).Value = fechaing;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@escolaridad", SqlDbType.VarChar).Value = escolaridad;
                command.Parameters.Add("@antecedentes", SqlDbType.VarChar).Value = antecedentes;
                command.Parameters.Add("@perscalif", SqlDbType.VarChar).Value = perscalif;
                command.Parameters.Add("@foto", SqlDbType.VarBinary, foto.Length).Value = foto;
                command.Parameters.Add("@auditoriso14001", SqlDbType.VarChar).Value = auditoriso14001;
                command.Parameters.Add("@idpuesto", SqlDbType.Int).Value = idpuesto;
                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;
                command.Parameters.Add("@certificaciones", SqlDbType.VarBinary, certificaciones.Length).Value = certificaciones; // Nuevo parámetro para las imágene

                command.ExecuteNonQuery();
            }
        }

        public void EditTrabajador(string numtarjeta, string nombre, string rfc, string escolaridad, string antecedentes, string perscalif, byte[] foto, string auditoriso14001, int idpuesto, int idarea, string activo, string id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Editar_Trabajador @numtarjeta, @nombre, @rfc, @escolaridad, @antecedentes, @perscalif, @foto, @auditoriso14001, @idpuesto, @idarea, @activo, @id";

                command.Parameters.Add("@numtarjeta", SqlDbType.VarChar).Value = numtarjeta;
                command.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@escolaridad", SqlDbType.VarChar).Value = escolaridad;
                command.Parameters.Add("@antecedentes", SqlDbType.VarChar).Value = antecedentes;
                command.Parameters.Add("@perscalif", SqlDbType.VarChar).Value = perscalif;
                command.Parameters.Add("@foto", SqlDbType.VarBinary, foto.Length).Value = foto;
                command.Parameters.Add("@auditoriso14001", SqlDbType.VarChar).Value = auditoriso14001;
                command.Parameters.Add("@idpuesto", SqlDbType.Int).Value = idpuesto;
                command.Parameters.Add("@idarea", SqlDbType.Int).Value = idarea;
                command.Parameters.Add("@activo", SqlDbType.VarChar).Value = activo;

                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;

                command.ExecuteNonQuery();
            }
        }

        //reportes
        public TrabajadorModel FormatoDC3(string numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ReporteDC3 @numficha";

                command.Parameters.Add("@numficha", SqlDbType.VarChar).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = reader[0].ToString(),
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
                            Id = reader[0].ToString(),
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
        public TrabajadorModel GetById(string numficha, string nomarea)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec BuscarTrabajadorEsp_PorArea @numficha, @nomarea";

                command.Parameters.Add("@numficha", SqlDbType.VarChar).Value = numficha;
                command.Parameters.Add("@nomarea", SqlDbType.VarChar).Value = nomarea;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = reader[0].ToString(),
                            Nombre = reader[1].ToString(),
                            Puesto = reader[2].ToString()                           
                        };
                    }
                }
            }
            return trabajador;
        }

        public byte[] ObtenerCertificacionesPorIdTrabajador(string numficha)
        {
            string sql = "SELECT certificaciones FROM trabajador WHERE id = @numficha";

            byte[] certificacionesSerializadas = null;

            using (var connection = GetConnection())
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@numficha", numficha);

                certificacionesSerializadas = (byte[])command.ExecuteScalar();
            }

            return certificacionesSerializadas;
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
                            Id = reader[0].ToString()
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
                            Id = reader[0].ToString(),
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
                            Id = reader[0].ToString(),
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
                            Id = reader[0].ToString(),
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
                            Id = reader[1].ToString(),
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
                            Id = reader[1].ToString(),
                            Nombre = reader[2].ToString(),                         
                            Puesto = reader[3].ToString()
                        };

                        personasCalif.Add(personalCalif);
                    }
                }
            }
            return personasCalif;
        }

        public TrabajadorModel GetTrabajador(string numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Ver_Trabajador @numficha";

                command.Parameters.Add("@numficha", SqlDbType.VarChar).Value = numficha;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        trabajador = new TrabajadorModel()
                        {
                            Id = reader[0].ToString(),
                            Foto = reader[1] as byte[],
                            NumTarjeta = reader[2].ToString(),
                            FechaIng = reader[3].ToString(),
                            antiguedadanios = (int)reader[4],
                            antiguedadmeses = (int)reader[5],
                            Categoria = reader[6].ToString(),
                            Escolaridad = reader[7].ToString(),
                            Nombre = reader[8].ToString(),
                            RFC = reader[9].ToString(),
                            Departamento = reader[10].ToString(),
                            Area = reader[11].ToString(),
                            Puesto = reader[12].ToString(),
                            Auditoriso14001 = reader[13] as bool? ?? false,
                            PersCalif = reader[14] as bool? ?? false,
                            Antecedentes = reader[15].ToString(),
                            Activo = reader[16] as bool? ?? false
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
                            Id = reader[0].ToString(),
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
                            Id = reader[1].ToString(),                           
                            Area = reader[2].ToString()
                        };

                        trabajadores.Add(trabajador);
                    }
                }
            }
            return trabajadores;
        }

        //reportes
        public TrabajadorModel GetTrabajadorHistorialCursos(string numficha)
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
                            Id = reader[0].ToString(),
                            Nombre = reader[1].ToString(),
                            Puesto = reader[2].ToString(),
                            Departamento = reader[3].ToString(),
                            Area = reader[4].ToString(),
                            FechaIng = reader[5].ToString()
                        };
                    }
                }
            }
            return trabajador;
        }

        public void AddCertificacion(string numficha, byte[] nuevaCertificacion)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "UPDATE trabajador SET certificaciones = certificaciones + @nuevaCertificacion WHERE id = @numficha";

                command.Parameters.Add("@nuevaCertificacion", SqlDbType.VarBinary, nuevaCertificacion.Length).Value = nuevaCertificacion;
                command.Parameters.Add("@numficha", SqlDbType.VarChar).Value = numficha;

                command.ExecuteNonQuery();
            }
        }

    }
}
