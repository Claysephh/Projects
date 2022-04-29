using System;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Capstone.DAO
{
    public class UserSqlDao : IUserDao
    {
        private readonly string connectionString;

        public UserSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        /// <summary>
        /// Returns a list of all the users on the app, not admin or owners
        /// </summary>
        /// <returns>List of users who are only users</returns>
        public List<ReturnUser> GetRegularUsers()
        {
            List<ReturnUser> returnUsers = new List<ReturnUser>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, user_role FROM users WHERE user_role = 'user';", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ReturnUser u = new ReturnUser()
                        {
                            UserId = Convert.ToInt32(reader["user_id"]),
                            Username = Convert.ToString(reader["username"]),
                            Role = Convert.ToString(reader["user_role"]),
                        };
                        returnUsers.Add(u);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnUsers;
        }
        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        User u = GetUserFromReader(reader);
                        returnUser = u;
                    }
                    if (returnUser != null && returnUser.Role == "disabled")
                    {
                        return null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnUser;
        }
        public User GetUser(int id)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt, user_role FROM users WHERE user_id = @user_id", conn);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        User u = GetUserFromReader(reader);
                        returnUser = u;
                    }
                    if (returnUser.Role == "disabled")
                    {
                        return null;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return returnUser;
        }
        public User AddUser(string username, string password, string role)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt, user_role) VALUES (@username, @password_hash, @salt, @user_role)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_role", role);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return GetUser(username);
        }
        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
                Role = Convert.ToString(reader["user_role"]),
            };
            return u;
        }

        public bool Disable(int id)
        {
            try
            {
                User old = GetUser(id);
                if (old != null)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE users SET user_role = 'disabled' WHERE user_id = @user_id;", conn);
                        cmd.Parameters.AddWithValue("@user_id", id);
                        cmd.ExecuteNonQuery();

                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw new Exception();
            }
        }
        public RegisterUser Edit(RegisterUser user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    IPasswordHasher passwordHasher = new PasswordHasher();
                    PasswordHash hash = passwordHasher.ComputeHash(user.Password);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE users SET username = @username, password_hash = @password_hash, salt = @salt, user_role = @user_role WHERE user_id = @user_id;", conn);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.Parameters.AddWithValue("@user_id", user.UserId);
                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@user_role", user.Role);
                    cmd.ExecuteNonQuery();
                }
                return user;
            }
            catch (NullReferenceException)
            {
                throw;
            }
            catch
            {
                throw;
            }
        }

        public List<int> GetWineryIdsFromOwnerId(int ownerId)
        {
            List<int> wineryId = new List<int>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT winery_id FROM owners WHERE owner_id = @owner_id ", conn);
                    cmd.Parameters.AddWithValue("@owner_id", ownerId);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        int i = Convert.ToInt32(r["winery_id"]);
                        wineryId.Add(i);
                    }
                }
            }
            catch
            {
                throw;
            }
            return wineryId;
        }
    }
}
