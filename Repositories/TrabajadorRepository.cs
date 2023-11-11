﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class TrabajadorRepository : RepositoryBase, ITrabajadorModel
    {
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

                command.Parameters.Add("@numtarjeta", SqlDbType.Int).Value = numtarjeta;

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
        public IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista)
        {
            List<TrabajadorModel> participantes = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec ParticipantesPorListaAsistencia @idlistacursos";

                command.Parameters.Add("@idlistacursos", SqlDbType.Int).Value = idlista;

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
                            Id = (int)reader[0],
                            Nombre = reader[1].ToString(),
                            Area = reader[2].ToString(),
                            Puesto = reader[3].ToString()
                        };

                        personasCalif.Add(personalCalif);
                    }
                }
            }
            return personasCalif;
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
