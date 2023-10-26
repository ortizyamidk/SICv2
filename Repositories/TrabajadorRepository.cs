using System;
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
        //ver trabajadores para tabla en vista CustomerView
        public IEnumerable<TrabajadorModel> GetByAll()
        {
            List<TrabajadorModel> trabajadores = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT T.id, T.nombre, A.nomarea, P.nompuesto " +
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
                            Nombre = reader[1].ToString(),
                            Area = reader[2].ToString(),
                            Puesto = reader[3].ToString()
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

        //participantes para CursoInfoGView
        public IEnumerable<TrabajadorModel> GetParticipantesListaA(int idlista)
        {
            List<TrabajadorModel> participantes = new List<TrabajadorModel>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT T.id, T.numtarjeta, T.nombre, P.nompuesto " +
                    "FROM trabajador AS T " +
                    "INNER JOIN puesto AS P " +
                    "ON T.idpuesto = P.id " +
                    "INNER JOIN cursotrabajador AS CT " +
                    "ON T.id = CT.idtrabajador " +
                    "INNER JOIN curso AS C " +
                    "ON CT.idcurso = C.id " +
                    "INNER JOIN curso_area AS CA " +
                    "ON C.id = CA.idcurso " +
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
