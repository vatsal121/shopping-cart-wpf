using ShoppingCartVatsalMeetJankiParth.Entities;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.DataAccess
{
    class LoginDataAccessor
    {
        private readonly string connectionString;

        public LoginDataAccessor()
        {
            connectionString = ConfigurationManager.ConnectionStrings["ShoppingDB_VMJP"].ConnectionString;
        }

        public bool UserExists(string username)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = "SELECT COUNT(Id) FROM dbo.Users WHERE Username = @Username; ";
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

            int count = (int)command.ExecuteScalar();

            return (count > 0);
        }

        public User Get(string username)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "SELECT Id, UserName, PasswordSalt, PasswordHash, Role, DateCreated, DateModified "
                + "FROM dbo.Users WHERE UserName = @Username; ";
            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = username;

            SqlDataReader reader = command.ExecuteReader();
            User user = null;

            if (reader.Read())
            {
                user = ExtractNextUser(reader);
            }

            return user;
        }

        private User ExtractNextUser(SqlDataReader reader)
        {
            User user = new User();
            user.Id = reader.GetInt32(0);
            user.Username = reader.GetString(1);
            byte[] salt = reader.GetValue(2) as byte[];
            byte[] hash = reader.GetValue(3) as byte[];
            user.Password = new PasswordHash(salt, hash);
            user.Role = (Enum.TryParse(reader.GetString(4), out UserRole role))
                ? role
                : UserRole.Customer;
            user.DateCreated = reader.GetDateTime(5);
            user.DateModified = reader.GetDateTime(6);
            return user;
        }

        public void Add(User user)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText =
                "INSERT INTO dbo.Users "
                + "( UserName, PasswordSalt, PasswordHash, Role,DateCreated, DateModified) "
                + "OUTPUT INSERTED.Id "
                + "VALUES( @Username, @PasswordSalt, @PasswordHash, @UserRole,@DateCreated, @DateModified); ";

            user.DateCreated = (DateTime)(user.DateModified = DateTime.UtcNow);

            command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
            command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary).Value = user.Password.Salt;
            command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = user.Password.Hash;
            command.Parameters.Add("@UserRole", SqlDbType.NVarChar).Value = user.Role.ToString();

            command.Parameters.Add("@DateCreated", SqlDbType.DateTime2).Value = user.DateCreated;
            command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = user.DateModified;
            user.Id = (int)command.ExecuteScalar();
        }

        public void ChangePassword(User user, string oldPassword, string newPassword)
        {
            if (PasswordUtility.CheckPassword(oldPassword, user.Password))
            {
                PasswordHash newPasswordHash = PasswordUtility.GeneratePasswordHash(newPassword);
                user.Password = newPasswordHash;

                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                using SqlCommand command = connection.CreateCommand();

                command.CommandText =
                    "UPDATE dbo.Users "
                    + "set PasswordSalt= @PasswordSalt, PasswordHash=@PasswordHash, DateModified=@DateModified "
                    + "Where Id=@Id";

                user.DateCreated = (DateTime)(user.DateModified = DateTime.UtcNow);

                command.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary).Value = user.Password.Salt;
                command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary).Value = user.Password.Hash;
                command.Parameters.Add("@DateModified", SqlDbType.DateTime2).Value = user.DateModified;
                command.ExecuteScalar();
            }
        }
    }
}
