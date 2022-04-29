using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WineryPopulator.DAO;

namespace WineryPopulator.DAO
{
    public class SqlDao : ISqlDao
    {
        public bool LogWineries(List<WineryData> wineries)
        {
            bool success = false;
            try
            {
				return success;
            }
            catch
            {
				throw;
            }
        }
		public IConfiguration Configuration { get; set; }
		public void CreateWinery(WineryData winery)
        {
			winery.autofill();
            try
            {
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(@"INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
													VALUES(@winery_name, @winery_country, @winery_address, @winery_city, @winery_state_abbr, @winery_zip, @winery_phone_number, @description, @image, @status);", conn);
					cmd.Parameters.AddWithValue("@winery_name", winery.name);
					cmd.Parameters.AddWithValue("@winery_country", winery.address.winery_country);
					cmd.Parameters.AddWithValue("@winery_address", winery.address.winery_address);
					cmd.Parameters.AddWithValue("@winery_city", winery.address.winery_city);
					cmd.Parameters.AddWithValue("@winery_state_abbr", winery.address.winery_state_abbr);
					cmd.Parameters.AddWithValue("@winery_zip", winery.address.winery_zip);
					cmd.Parameters.AddWithValue("@winery_phone_number", winery.phone_number);
					cmd.Parameters.AddWithValue("@image", winery.photos[0].photo_reference);
					cmd.Parameters.AddWithValue("@status", winery.status);
					cmd.Parameters.AddWithValue("@description", winery.description);
					cmd.ExecuteNonQuery();
				}
            }
            catch
            {
				throw;
            }
        }

		string connectionString = "Server=.\\SQLEXPRESS;Database=final_capstone;Trusted_Connection=True";
		public void CreateDatabase()
        {
            try
            {
				using (SqlConnection conn = new SqlConnection(connectionString))
                {
					conn.Open();
					SqlCommand cmd = new SqlCommand(DataBase, conn);
					cmd.ExecuteNonQuery();
                }
            }
            catch
            {
				throw;
            }
        }
		public void FinishDatabase()
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					conn.Open();
					SqlCommand cmd = new SqlCommand(DatabaseFill, conn);
					cmd.ExecuteNonQuery();
				}
			}
			catch
			{
				throw;
			}
		}
		private string DataBase = @"
		USE master

		IF DB_ID('final_capstone') IS NOT NULL
		BEGIN
			ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
			DROP DATABASE final_capstone;
		END;
		
		CREATE DATABASE final_capstone;

		
		USE final_capstone;

		CREATE TABLE users (
			user_id int IDENTITY(1,1) NOT NULL,
			username varchar(50) NOT NULL,
			password_hash varchar(200) NOT NULL,
			salt varchar(200) NOT NULL,
			user_role varchar(50) NOT NULL
			CONSTRAINT PK_user PRIMARY KEY (user_id)
		);
		
		CREATE TABLE wineries (
			winery_id int IDENTITY(1000,1) NOT NULL,
			winery_name varchar(100) NOT NULL,
			winery_country varchar(50) NOT NULL,
			winery_address varchar(50) NOT NULL,
			winery_city varchar(50) NOT NULL,
			winery_state_abbr varchar(5) NOT NULL,
			winery_zip int NOT NULL,
			winery_phone_number varchar(15) NOT NULL,
			description varchar(8000) NOT NULL,
			image varchar(1000),
			status bit NOT NULL
			CONSTRAINT FK_winery PRIMARY KEY (winery_id)
		);
		
		CREATE TABLE winery_reviews(
			user_id int NOT NULL,
			winery_id int NOT NULL,
			review varchar(1000) NOT NULL,
			grapes int NOT NULL CHECK(grapes >= 0 AND grapes <= 5)
			CONSTRAINT FK_user_id FOREIGN KEY(user_id) REFERENCES users(user_id),
			CONSTRAINT FK_winery_review_id FOREIGN KEY(winery_id) REFERENCES wineries(winery_id)
			);
		CREATE TABLE owner_request(
			request_id int IDENTITY(1,1) NOT NULL,
			requester_id int NOT NULL,
			winery_id int NOT NULL,
			request_comment varchar(1000) NOT NULL,
			status varchar(50) NOT NULL,
			CONSTRAINT FK_requester_id FOREIGN KEY(requester_id) REFERENCES users(user_id),
			CONSTRAINT FK_winery_id FOREIGN KEY(winery_id) REFERENCES wineries(winery_id),
			CONSTRAINT PK_request_id PRIMARY KEY(request_id)
		);
		
		CREATE TABLE owners(
			owner_id int NOT NULL,
			winery_id int NOT NULL,
			request_id int,
			CONSTRAINT FK_owner_id FOREIGN KEY (owner_id) REFERENCES users (user_id),
			CONSTRAINT FK_winery_owner_id FOREIGN KEY (winery_id) REFERENCES wineries (winery_id),
			CONSTRAINT FK_request_id FOREIGN KEY (request_id) REFERENCES owner_request (request_id),
			);
		
			CREATE TABLE wines(
			wine_id int IDENTITY(10000, 1) NOT NULL,
			winery_id int NOT NULL,
			name varchar(100) UNIQUE NOT NULL,
			style varchar(50) NOT NULL,
			description varchar(1000),
			image varchar(1000),
			status bit NOT NULL,
			CONSTRAINT FK_wine_winery_id FOREIGN KEY(winery_id) REFERENCES wineries(winery_id),
			CONSTRAINT PK_wine_id PRIMARY KEY(wine_id)
			);
		

		
		
		
		USE final_capstone;

		DELETE FROM wines;
		DELETE FROM owners;
		DELETE FROM owner_request;
		DELETE FROM users;
		DELETE FROM wineries;

		
	";
	private string DatabaseFill = @"
		USE final_capstone;
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('user1','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('JoeDirt','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('JohnDoe','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('JamesMaddy','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('PatrickStarrr','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('JFalco','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('DaveFerreira','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('JainDoe','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');


		INSERT INTO users(username, password_hash, salt, user_role) VALUES('ClaytonTheOwner','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','owner');
		INSERT INTO users(username, password_hash, salt, user_role) VALUES('owner1','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','owner');

		INSERT INTO users(username, password_hash, salt, user_role) VALUES('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');


		INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
		VALUES(
		'Dave''s Place',
		'USA',
		'2751 Dave Road',
		'Dave Place',
		'OH',
		 95722,
		'123-456-7890',
		'This winery is owned by David Ferreira', '', 1);


		INSERT INTO owner_request(requester_id, winery_id, request_comment, status) VALUES((SELECT user_id FROM users WHERE username = 'JohnDoe'), 1010, 'Hi, my name is John Doe. I own this winery. Will you please make me the owner?', 'pending');
		INSERT INTO owner_request(requester_id, winery_id, request_comment, status) VALUES((SELECT user_id FROM users WHERE username = 'DaveFerreira'), (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'please make me owner of Dave''s Place. It is my winery.', 'pending');
		INSERT INTO owner_request(requester_id, winery_id, request_comment, status) VALUES((SELECT user_id FROM users WHERE username = 'PatrickStarrr'), 1013, 'please make me owner', 'approved');
		INSERT INTO owner_request(requester_id, winery_id, request_comment, status) VALUES((SELECT user_id FROM users WHERE username = 'JainDoe'), 1012, 'please make me owner', 'approved');

		INSERT INTO owners(owner_id, winery_id, request_id) VALUES((SELECT user_id FROM users WHERE username = 'PatrickStarrr'), 1011, (SELECT request_id FROM owner_request WHERE requester_id = (SELECT user_id FROM users WHERE username = 'owner1')));
		INSERT INTO owners(owner_id, winery_id, request_id) VALUES((SELECT user_id FROM users WHERE username = 'JainDoe'), 1012, (SELECT request_id FROM owner_request WHERE requester_id = (SELECT user_id FROM users WHERE username = 'TheBigC')));



		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('White Galor', (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'Full-Bodied White Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNHy6suHjRQiP1wibvD6FDuEwBfL2gHMxF6g&usqp=CAU', 0);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('The Super Sweet', (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'Full-Bodied White Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQl2Rtunu6wEzflQn4DC0RymU_wYWShgG2-eA&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('Dave''s place Chardonnay', (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'Full-Bodied White Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREfOiLo0yiLn7Gj9C6sKP59c1alAKzsyt16g&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('Dave''s place Merlot', (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'Light-Bodied Red Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjXGmOx_pfMasktzeOavhppL3vKmtxpncFHA&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('THE Red One', (SELECT winery_id FROM wineries WHERE winery_name = 'Dave''s Place'), 'Light-Bodied Red Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQN-APuidAoYVfShidcdoJJ6R7xalKla_plyQ&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('The Good Stuff', 1011, 'Medium-Bodied Red Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfY75OKNg6m51htNSpTpEZFv2JimyIFrPTcA&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('2019 Bordeaux', 1011, 'Medium-Bodied Red Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8UvEz3tMka-cjhU6n3Jn0qdC5AR3VOi_oPg&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('2020 Rose', 1011, 'Dessert Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUj98rZxRMo38jHR2nOlD7AIxYptqrDDoAjg&usqp=CAU',1);
		INSERT INTO wines(name, winery_id, style, description, image, status)
		VALUES('2010 Merlot', 1011, 'Medium-Bodied Red Wine', 'What a wine', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRK6dngFIYHzINk2SWCbgUkoQ5UpxB2Gh2E8A&usqp=CAU' ,1);
		";

	}
	
}
