USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END


CREATE DATABASE final_capstone
GO

USE final_capstone
GO
--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE wineries (
	winery_id int IDENTITY(1000,1) NOT NULL,
<<<<<<< HEAD
	winery_name varchar(100)UNIQUE NOT NULL,
=======
	winery_name varchar (100) UNIQUE NOT NULL,
>>>>>>> eb64480ebcd08b4a0774672ad7947967486889f4
	winery_country varchar(50) NOT NULL,
	winery_address varchar(50) NOT NULL,
	winery_city varchar(50) NOT NULL,
	winery_state_abbr varchar(5) NOT NULL,
	winery_zip int NOT NULL,
	winery_phone_number varchar(15) NOT NULL,
	description varchar(8000) NOT NULL,
	CONSTRAINT FK_winery PRIMARY KEY (winery_id)
)

CREATE TABLE winery_reviews(
	user_id int NOT NULL,
	winery_id int NOT NULL,
	review varchar(1000) NOT NULL,
	--Grapes instead of "stars" for ratings, because Clayton is cool!
	grapes int NOT NULL CHECK(grapes >= 0 AND grapes <= 5)
	CONSTRAINT FK_user_id FOREIGN KEY (user_id) REFERENCES users (user_id),
	CONSTRAINT FK_winery_review_id FOREIGN KEY (winery_id) REFERENCES wineries (winery_id)
	)
CREATE TABLE owner_request (
	request_id int IDENTITY(1,1) NOT NULL,
	requester_id int NOT NULL,
	winery_id int NOT NULL,
	request_comment varchar(1000) NOT NULL,
	status varchar(50) NOT NULL,
	CONSTRAINT FK_requester_id FOREIGN KEY (requester_id) REFERENCES users (user_id),
	CONSTRAINT FK_winery_id FOREIGN KEY (winery_id) REFERENCES wineries (winery_id),
	CONSTRAINT PK_request_id PRIMARY KEY (request_id)
)

CREATE TABLE owners(
	owner_id int NOT NULL,
	winery_id int NOT NULL,
	request_id int,
	CONSTRAINT FK_owner_id FOREIGN KEY (owner_id) REFERENCES users (user_id),
	CONSTRAINT FK_winery_owner_id FOREIGN KEY (winery_id) REFERENCES wineries (winery_id),
	CONSTRAINT FK_request_id FOREIGN KEY (request_id) REFERENCES owner_request (request_id),
	)

	CREATE TABLE wines(
	wine_id int IDENTITY(10000, 1) NOT NULL,
	winery_id int NOT NULL,
	name varchar(100) UNIQUE NOT NULL,
	style varchar(50) NOT NULL,
	description varchar(1000),
	image varchar(1000),
	status bit NOT NULL,
	CONSTRAINT FK_wine_winery_id FOREIGN KEY (winery_id) REFERENCES wineries (winery_id),
	CONSTRAINT PK_wine_id PRIMARY KEY (wine_id)
	)

GO



