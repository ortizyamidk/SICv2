using System;
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
        public TrabajadorModel FormatoDC3(int numficha)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT T.id, T.nombre, T.rfc, P.nompuesto " +
                                    "FROM trabajador AS T " +
                                    "INNER JOIN puesto AS P " +
                                    "ON T.idpuesto = P.id " +
                                    "WHERE T.id = @numficha";

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
                command.CommandText = "SELECT T.id, T.numtarjeta, T.nombre, A.nomarea, P.nompuesto " +
                                    "FROM trabajador AS T " +
                                    "INNER JOIN area AS A ON T.idarea = A.id " +
                                    "INNER JOIN puesto AS P ON T.idpuesto = P.id";

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

        //ver trabajador especifico para vista PersonalInfoView
        public TrabajadorModel GetById(int numficha, string nomarea)
        {
            TrabajadorModel trabajador = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT T.id, T.nombre, P.nompuesto " +
                    "FROM trabajador AS T " +
                    "INNER JOIN puesto AS P " +
                    "ON T.idpuesto = P.id " +
                    "INNER JOIN area AS A " +
                    "ON T.idarea = A.id " +
                    "WHERE T.id = @numficha AND A.nomarea = @nomarea";

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
                command.CommandText = "SELECT T.id, T.numtarjeta, T.nombre, P.nompuesto, A.nomarea " +
                    "FROM curso AS C " +
                    "LEFT JOIN instructor AS I " +
                    "ON C.idinstructor = I.id " +
                    "INNER JOIN cursotrabajador AS CT " +
                    "ON C.id = CT.idcurso " +
                    "INNER JOIN trabajador AS T " +
                    "ON CT.idtrabajador = T.id " +
                    "INNER JOIN puesto AS P " +
                    "ON T.idpuesto = P.id " +
                    "INNER JOIN area AS A " +
                    "ON T.idarea = A.id " +
                    "WHERE C.id = @idcurso";

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
                command.CommandText = "SELECT T.id, T.numtarjeta, T.nombre, P.nompuesto, A.nomarea " +
                    "FROM curso AS C " +
                    "LEFT JOIN instructor AS I " +
                    "ON C.idinstructor = I.id " +
                    "INNER JOIN cursotrabajador AS CT " +
                    "ON C.id = CT.idcurso " +
                    "INNER JOIN trabajador AS T " +
                    "ON CT.idtrabajador = T.id " +
                    "INNER JOIN puesto AS P " +
                    "ON T.idpuesto = P.id " +
                    "INNER JOIN area AS A " +
                    "ON T.idarea = A.id " +
                    "WHERE C.id = @idcurso AND A.nomarea = @area";

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
                command.CommandText = "SELECT DISTINCT T.id, T.numtarjeta, T.nombre, P.nompuesto " +
                    "FROM curso_area AS CA " +
                    "INNER JOIN area AS A " +
                    "ON CA.idarea = A.id " +
                    "INNER JOIN trabajador AS T " +
                    "ON A.id = T.idarea " +
                    "INNER JOIN puesto AS P " +
                    "ON T.idpuesto = P.id " +
                    "INNER JOIN cursotrabajador AS CT " +
                    "ON T.id = CT.idtrabajador " +
                    "WHERE CA.id = @idlistacursos";

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
    }
}
