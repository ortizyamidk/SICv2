﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF_LoginForm.Repositories
{
    public class InstructorRepository : RepositoryBase, IInstructorRepository
    {
        public IEnumerable<InstructorModel> GetByAll()
        {
            List<InstructorModel> instructores = new List<InstructorModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from instructor;";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstructorModel instructor = new InstructorModel()
                        {
                            Id = (int) reader[0],
                            NomInstr = reader[1].ToString(),
                            RFC = reader[2].ToString(),
                            TipoInstr = reader[3].ToString(),
                            NomCia = reader[4].ToString()
                        };
                        instructores.Add(instructor);
                    }
                }
            }
            return instructores;
        }

        InstructorModel IInstructorRepository.GetById(int id)
        {
            InstructorModel instructor = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from instructor where id = @id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        instructor = new InstructorModel()
                        {
                            Id = (int) reader[0],
                            NomInstr = reader[1].ToString(),
                            RFC = reader[2].ToString(),
                            TipoInstr = reader[3].ToString(),
                            NomCia= reader[4].ToString()
                        };
                    }
                }
            }
            return instructor;
        }
    }
}
