using System;
using System.Collections.Generic;
using Capstone.Models;
using System.Data.SqlClient;

namespace Capstone.DAO
{
    public class WinerySqlDao : IWineryDao
    {
        private readonly string connectionString;
        public WinerySqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public Winery GetWinery(string wineryName)
        {
            Winery returnWinery = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wineries 
                                                    WHERE winery_name = @winery_name
                                                    ", conn);

                    cmd.Parameters.AddWithValue("@winery_name", wineryName);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnWinery = GetWineryFromReader(reader);
                        if (returnWinery.Status == false)
                        {
                            returnWinery = null;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnWinery;
        }
        public Winery CreateWinery(Winery newWinery)
        {

            try
            {
                Winery winery = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
                                                    OUTPUT INSERTED.winery_id
                                                    VALUES(@winery_name, @winery_country, @winery_address, @winery_city, @winery_state_abbr, @winery_zip, @winery_phone_number, @description, @image, 1);", conn);
                    cmd.Parameters.AddWithValue("@winery_name", newWinery.WineryName);
                    cmd.Parameters.AddWithValue("@winery_country", newWinery.WineryCountry);
                    cmd.Parameters.AddWithValue("@winery_state_abbr", newWinery.WineryStateAbbr);
                    cmd.Parameters.AddWithValue("@winery_address", newWinery.WineryAddress);
                    cmd.Parameters.AddWithValue("@winery_city", newWinery.WineryCity);
                    cmd.Parameters.AddWithValue("@winery_zip", newWinery.WineryZip);
                    cmd.Parameters.AddWithValue("@winery_phone_number", newWinery.WineryPhoneNumber);
                    cmd.Parameters.AddWithValue("@description", newWinery.Description);
                    cmd.Parameters.AddWithValue("@image", newWinery.Image);
                    var id = cmd.ExecuteScalar();

                    if (id != null)
                    {
                        winery = GetWineryById(Convert.ToInt32(id));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return winery;
            }
            catch (SqlException)
            {
                throw;
            }
        }
        public List<Winery> GetWineries()
        {
            try
            {
                List<Winery> wineries = new List<Winery>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wineries", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Winery w = GetWineryFromReader(reader);
                        if (w.Status == true)
                        {
                            wineries.Add(w);
                        }
                    }
                    return wineries;
                }
            }
            catch
            {
                throw;
            }
        }

        public Winery GetWineryById(int wineryid)
        {
            try
            {
                Winery winery = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * FROM wineries 
                                                    WHERE winery_id = @winery_id
                                                    ", conn);

                    cmd.Parameters.AddWithValue("@winery_id", wineryid);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        winery = GetWineryFromReader(reader);
                        if (winery.Status == false)
                        {
                            winery = null;
                        }
                    }
                }
                return winery;
            }
            catch
            {
                throw;
            }
        }
        public Winery UpdateWineryById(Winery updatedWinery)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"UPDATE wineries SET winery_name = @winery_name, winery_country = @winery_country, 
                    winery_address = @winery_address, winery_city = @winery_city, winery_state_abbr = @winery_state_abbr, winery_zip = @winery_zip, 
                    winery_phone_number = @winery_phone_number, description = @description, image = @image WHERE winery_id = @winery_id;
                                                    ", conn);
                    cmd.Parameters.AddWithValue("@winery_name", updatedWinery.WineryName);
                    cmd.Parameters.AddWithValue("@winery_country", updatedWinery.WineryCountry);
                    cmd.Parameters.AddWithValue("@winery_state_abbr", updatedWinery.WineryStateAbbr);
                    cmd.Parameters.AddWithValue("@winery_address", updatedWinery.WineryAddress);
                    cmd.Parameters.AddWithValue("@winery_city", updatedWinery.WineryCity);
                    cmd.Parameters.AddWithValue("@winery_zip", updatedWinery.WineryZip);
                    cmd.Parameters.AddWithValue("@winery_phone_number", updatedWinery.WineryPhoneNumber);
                    cmd.Parameters.AddWithValue("@description", updatedWinery.Description);
                    cmd.Parameters.AddWithValue("@winery_id", updatedWinery.WineryId);
                    cmd.Parameters.AddWithValue("@image", updatedWinery.Image);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        updatedWinery = GetWineryFromReader(reader);
                    }
                }
                return updatedWinery;
            }
            catch
            {
                throw;
            }
        }
        public bool DeleteWinery(int wineryId)
        {
            bool success = false;


            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"BEGIN TRANSACTION;
                                            UPDATE wineries SET status = 0 WHERE winery_id = @winery_id;
                                            ROLLBACK", conn);
                    cmd.Parameters.AddWithValue("@winery_id", wineryId);
                    int i = cmd.ExecuteNonQuery();
                    if (i == 1)
                    {
                        success = true;
                        SqlCommand commit = new SqlCommand("UPDATE wineries SET status = 0 WHERE winery_id = @winery_id;", conn);
                        commit.Parameters.AddWithValue("@winery_id", wineryId);
                        commit.ExecuteNonQuery();
                    }
                    else if (i > 1)
                    {
                        throw new Exception("There was an API SQL mistake.");
                    }
                }
            }
            catch
            {
                throw;
            }

            return success;


        }
        public bool MakeOwner(Winery winery, int userId)
        {
            bool success = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        INSERT INTO owners (owner_id, winery_id) VALUES (@owner_id, @winery_id);
                        ", conn);
                    cmd.Parameters.AddWithValue("@owner_id", userId);
                    cmd.Parameters.AddWithValue("@winery_id", winery.WineryId);
                    int i = cmd.ExecuteNonQuery();
                    if(i == 1)
                    {
                        success = true;
                    }
                }
            }
            catch
            {
                throw;
            }
            return success;
        }

        private Winery GetWineryFromReader(SqlDataReader reader)
        {
            Winery w = new Winery()
            {
                WineryId = Convert.ToInt32(reader["winery_id"]),
                WineryName = Convert.ToString(reader["winery_name"]),
                WineryCountry = Convert.ToString(reader["winery_country"]),
                WineryAddress = Convert.ToString(reader["winery_address"]),
                WineryStateAbbr = Convert.ToString(reader["winery_state_abbr"]),
                WineryCity = Convert.ToString(reader["winery_city"]),
                WineryZip = Convert.ToInt32(reader["winery_zip"]),
                WineryPhoneNumber = Convert.ToString(reader["winery_phone_number"]),
                Description = Convert.ToString(reader["description"]),
                Image = Convert.ToString(reader["image"]),
                Status = Convert.ToBoolean(reader["status"])
            };
            return w;
        }
    }
}




