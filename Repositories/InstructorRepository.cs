using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WPF_LoginForm.Repositories
{
    public class InstructorRepository : RepositoryBase, IInstructorRepository
    {
        public void AddInstructor(int id, string nominstr, string rfc, string tipo, string compania)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Insertar_Instructor @id, @nominstr, @rfc, @tipo, @compania";

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.Parameters.Add("@nominstr", SqlDbType.VarChar).Value = nominstr;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@tipo", SqlDbType.VarChar).Value = tipo;
                command.Parameters.Add("@compania", SqlDbType.VarChar).Value = compania;

                command.ExecuteNonQuery();
            }
        }

        public void EditInstructor(string nominstr, string rfc, string tipo, string compania, int id)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "exec Editar_Instructor @nominstr, @rfc, @tipo, @compania, @id";

                command.Parameters.Add("@nominstr", SqlDbType.VarChar).Value = nominstr;
                command.Parameters.Add("@rfc", SqlDbType.VarChar).Value = rfc;
                command.Parameters.Add("@tipo", SqlDbType.VarChar).Value = tipo;
                command.Parameters.Add("@compania", SqlDbType.VarChar).Value = compania;

                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }

        //ver todos los instructores dados de alta en bd
        public IEnumerable<InstructorModel> GetByAll()
        {
            List<InstructorModel> instructores = new List<InstructorModel>();
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * from INSTRUCTOR";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        InstructorModel instructor = new InstructorModel()
                        {
                            Id = (int)reader[0],
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

        //Obtener el id del instructor seleccionado en el combobox
        public InstructorModel GetIdByNombre(string nombreinstr)
        {
            InstructorModel instrid = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT id FROM instructor WHERE nominstr = @nombreinstr";
                command.Parameters.Add("@nombreinstr", SqlDbType.VarChar).Value = nombreinstr;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        instrid = new InstructorModel()
                        {
                            Id = (int)reader[0]
                        };
                    }
                }
            }
            return instrid;
        }

        //ver datos del instructor especifico
        InstructorModel IInstructorRepository.GetById(int id)
        {
            InstructorModel instructor = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM instructor WHERE id = @id";
                command.Parameters.Add("@id", SqlDbType.VarChar).Value = id;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        instructor = new InstructorModel()
                        {
                            Id = (int)reader[0],
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
