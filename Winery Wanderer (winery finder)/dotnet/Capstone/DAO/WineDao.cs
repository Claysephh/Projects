using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class WineDao : IWineDao
    {
        readonly string sqlConnection;
        public WineDao(string _sqlConnection)
        {
            sqlConnection = _sqlConnection;
        }
        public Wine CreateWine(Wine wine)
        {
            try
            {
                Wine newWine = null;
                using (SqlConnection conn = new SqlConnection(sqlConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO wines (name, winery_id, style, description, image, status) OUTPUT INSERTED.wine_id VALUES (@name, @winery_id, @style, @description, @image_url, 1);", conn);
                    cmd.Parameters.AddWithValue("@name", wine.Name);
                    cmd.Parameters.AddWithValue("@winery_id", wine.WineryId);
                    cmd.Parameters.AddWithValue("@style", wine.Style);
                    cmd.Parameters.AddWithValue("@description", wine.Description);
                    cmd.Parameters.AddWithValue("@image_url", wine.Image);
                    int newWineId = Convert.ToInt32(cmd.ExecuteScalar());
                    newWine = wine;
                    newWine.WineId = newWineId;
                }
                return newWine;
            }
            catch
            {
                throw;
            }
        }

        public bool DeleteWine(int id)
        {
            try
            {
                bool successful = false;
                Wine wine = GetWine(id);
                wine.Status = 0;
                if(UpdateWine(wine))
                {
                    successful = true;
                }
                return successful;
            }
            catch
            {
                throw;
            }
        }

        public List<Wine> GetAllWines()
        {
            try
            {
                List<Wine> wines = new List<Wine>();
                using (SqlConnection conn = new SqlConnection(sqlConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wines", conn);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        Wine w = GetWineFromReader(r);
                        if(w != null)
                        {
                            wines.Add(w);
                        }
                    }
                }
                return wines;
            }
            catch
            {
                throw;
            }
        }

        public Wine GetWine(string Name)
        {
            try
            {
                Wine newWine = null;
                using (SqlConnection conn = new SqlConnection(sqlConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wines WHERE name = @name;", conn);
                    cmd.Parameters.AddWithValue("@name", Name);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        newWine = GetWineFromReader(r);
                    }
                }
                return newWine;
            }
            catch
            {
                throw;
            }
        }

        public Wine GetWine(int id)
        {
            try
            {
                Wine newWine = null;
                using (SqlConnection conn = new SqlConnection(sqlConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wines WHERE wine_id = @wine_id;", conn);
                    cmd.Parameters.AddWithValue("@wine_id", id);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        newWine = GetWineFromReader(r);
                    }
                }
                return newWine;
            }
            catch
            {
                throw;
            }
        }
        public bool UpdateWine(Wine wine)
        {
            try
            {
                bool successful = false;
                using(SqlConnection conn = new SqlConnection(sqlConnection))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE wines SET name = @name, style = @style, description = @description, image = @image, status = @status WHERE wine_id = @wine_id;", conn);
                    cmd.Parameters.AddWithValue("@name", wine.Name);
                    cmd.Parameters.AddWithValue("@style", wine.Style);
                    cmd.Parameters.AddWithValue("@description", wine.Description);
                    cmd.Parameters.AddWithValue("@image", wine.Image);
                    cmd.Parameters.AddWithValue("@wine_id", wine.WineId);
                    cmd.Parameters.AddWithValue("@status", wine.Status);
                    int i = cmd.ExecuteNonQuery();
                    if(i == 1)
                    {
                        successful = true;
                    }
                }
                return successful;
            }
            catch
            {
                return false;
            }
        }
        private Wine GetWineFromReader(SqlDataReader reader)
        {
            try
            {
                Wine w = null;
                if (Convert.ToBoolean(reader["status"]))
                {
                    w = new Wine()
                    {
                        WineId = Convert.ToInt32(reader["wine_id"]),
                        Name = Convert.ToString(reader["name"]),
                        WineryId = Convert.ToInt32(reader["winery_id"]),
                        Style = Convert.ToString(reader["style"]),
                        Description = Convert.ToString(reader["description"]),
                        Image = Convert.ToString(reader["image"]),
                    };
                }

                return w;
            }
            catch
            {
                throw;
            }
        }
    }
}
