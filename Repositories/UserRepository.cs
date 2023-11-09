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
        

        public bool AuthenticateUser(NetworkCredential credential)
        {
            bool validUser;

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT * FROM [user] WHERE Username=@username AND Password=@contra";
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = credential.UserName;
                command.Parameters.Add("@contra", SqlDbType.NVarChar).Value = credential.Password;

                validUser = command.ExecuteScalar() == null ? false : true;
            }

            return validUser;
        }     

        public UserModel GetByUsername(string username)
        {
            UserModel user = null;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT U.rol, A.nomarea " +
                                    "FROM [user] AS U " +
                                    "INNER JOIN area AS A ON U.idarea = A.id " +
                                    "WHERE U.Username = @username";

                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel()
                        {
                            Rol = reader[0].ToString(),
                            Area = reader[1].ToString()
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

       
    }
}
