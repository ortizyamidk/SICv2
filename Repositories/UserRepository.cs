using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WPF_LoginForm.Models;

namespace WPF_LoginForm.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public void Add(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT Id, Username, Rol FROM [user] WHERE Username=@username AND Password=@contra";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@contra", SqlDbType.NVarChar).Value = credential.Password;

                validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }

        public void Edit(UserModel userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserModel> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT U.Username, U.rol, T.nombre, D.nomdepto " +
                        "FROM [user] AS U " +
                        "INNER JOIN trabajador AS T ON U.idtrabajador = T.id " +
                        "INNER JOIN area AS A ON T.idarea = A.id " +
                        "INNER JOIN departamento AS D ON A.iddpto = D.id " +
                        "WHERE U.Username = @username";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Username = reader[0].ToString(),
                            Rol = reader[1].ToString(),
                            TrabajadorNombre = reader[2].ToString(),
                            Departamento = reader[3].ToString(),
                        };
                    }
                }
            }
            return user;
        }

        public IList<string> GetUserRoles(string username)
        {
            IList<string> roles = new List<string>();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT rol FROM [user] WHERE Username = @user";
                command.Parameters.Add("@user", SqlDbType.NVarChar).Value = username;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(reader["rol"].ToString());
                    }
                }
            }

            return roles;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
