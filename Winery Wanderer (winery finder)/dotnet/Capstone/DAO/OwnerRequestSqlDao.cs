using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public class OwnerRequestSqlDao : IOwnerRequestDao
    {
        private readonly string connectionString;
        public OwnerRequestSqlDao(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public OwnerRequest CreateRequestToBecomeOwner(OwnerRequest request)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO owner_request (requester_id, winery_id, request_comment, status) 
                                                    OUTPUT INSERTED.request_id
                                                    VALUES(@user_id, @winery_id, @comment, 'pending');", conn);//
                    cmd.Parameters.AddWithValue("@user_id", request.RequesterId);
                    cmd.Parameters.AddWithValue("@winery_id", request.WineryId);
                    cmd.Parameters.AddWithValue("@comment", request.Comment);
                    request.RequestId = Convert.ToInt32(cmd.ExecuteScalar());
                    return request;
                }
            }
            catch
            {
                throw;
            }
        }
        public OwnerRequest GetRequestById(int id)
        {
            try
            {
                OwnerRequest request = null;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT request_id, requester_id, username, wineries.winery_id, winery_name, status, request_comment FROM owner_request
                                                    JOIN users ON requester_id = users.user_id
                                                    JOIN  wineries ON wineries.winery_id = owner_request.winery_id WHERE request_id = @request_id", conn);
                    cmd.Parameters.AddWithValue("@request_id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        request = GetOwnerRequestFromReader(reader);
                    }
                }
                return request;
            }
            catch
            {
                throw new Exception();

            }
        }
        public List<OwnerRequest> GetRequests()
        {
            try
            {
                List<OwnerRequest> requests = new List<OwnerRequest>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"SELECT request_id, requester_id, username, wineries.winery_id, winery_name, owner_request.status, request_comment FROM owner_request
                                                    JOIN users ON requester_id = users.user_id
                                                    JOIN  wineries ON wineries.winery_id = owner_request.winery_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        requests.Add(GetOwnerRequestFromReader(reader));
                    }
                }
                return requests;
            }
            catch
            {
                throw new Exception();

            }
        }
        public void ApproveRequest(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"BEGIN TRANSACTION
                        UPDATE owner_request SET status = 'approved' WHERE request_id = @request_id;
                        UPDATE users SET user_role = 'owner' WHERE user_id = (SELECT requester_id FROM owner_request WHERE request_id = @request_id);
                        INSERT INTO owners (owner_id, winery_id, request_id) VALUES ((SELECT requester_id FROM owner_request WHERE request_id = @request_id), (SELECT winery_id FROM owner_request WHERE request_id = @request_id), @request_id);
                        COMMIT", conn);

                    cmd.Parameters.AddWithValue("@request_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
        public void DeclineRequest(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"UPDATE owner_request SET status = 'declined' WHERE request_id = @request_id", conn);
                    cmd.Parameters.AddWithValue("@request_id", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception();

            }
        }
        private OwnerRequest GetOwnerRequestFromReader(SqlDataReader reader)
        {
            OwnerRequest request = new OwnerRequest()
            {
                RequestId = Convert.ToInt32(reader["request_id"]),
                RequesterId = Convert.ToInt32(reader["requester_id"]),
                Username = Convert.ToString(reader["username"]),
                WineryName = Convert.ToString(reader["winery_name"]),
                WineryId = Convert.ToInt32(reader["winery_id"]),
                Comment = Convert.ToString(reader["request_comment"]),
                Status = Convert.ToString(reader["status"])
            };

            return request;
        }
    }
}
