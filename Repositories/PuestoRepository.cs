﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class PuestoRepository : RepositoryBase, IPuestoRepository
    {
        public PuestoModel GetIdByNombrePuesto(string nompuesto)
        {
            PuestoModel puesto = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM puesto WHERE nompuesto = @nompuesto";

                command.Parameters.Add("@nompuesto", SqlDbType.VarChar).Value = nompuesto;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        puesto = new PuestoModel()
                        {
                            Id = (int)reader[0]

                        };
                    }
                }
            }
            return puesto;
        }
    }
}
