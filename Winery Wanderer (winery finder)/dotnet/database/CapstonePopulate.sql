
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
	winery_name varchar(100) UNIQUE NOT NULL,
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



USE final_capstone
GO
DELETE FROM wines;
DELETE FROM owners;
DELETE FROM owner_request;
DELETE FROM users;
DELETE FROM wineries;
GO
--users
--All Passwords === password
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user1','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user2','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user3','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user4','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user5','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user6','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user7','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user8','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','user');


INSERT INTO users (username, password_hash, salt, user_role) VALUES ('owner1','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','owner');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('owner2','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','owner');

INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--Wineries
--Ohio
INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'North Coast Wine Club',
'USA',
'30700 Bainbridge Rd.',
'Solon',
'OH',
44139,
'440-821-4822',
'We make Premium, Organic  Wines as a group. You are not alone in your basement or garage. We buy Premium Grapes from all over the world. We don''t 
make wine from kits. It does take extra time, but isn''t that true for all the best things in life?', 'http://storage.googleapis.com/cdn.vinoshipper.com/winery/d363a691-083e-4f99-8b6c-37328fbba8a5.png', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Autumn Rush Vinyard',
'USA',
'5686 Dutch Ln',
'Johnstown',
'OH',
43031,
'740-328-5777',
'Autumn Rush Vineyard serves authentic wines produced on our estate. We also serve hard cider, wine slushies and wine and cider cocktails.
The Sip Is Just The Beginning….to Great gatherings, Great memories, and Great experiences! https://www.autumnrushvineyard.com/', 'https://ohio.org/static/uploads/068t000000Fl07GAAR.jpg', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Heron Creek Winery/Brady Vineyards',
'USA',
'52185 Griggs Rd. W.',
'Wellington',
'OH',
44090,
'440-506-7022',
'Coming Soon https://heroncreekwine.com/', 'https://images.squarespace-cdn.com/content/v1/5b7efaff55b02cdb6c3f4174/1635798103499-DCK8FDU9ZAFPBNGW3ELN/logo+web.jpg', 0);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Benny Vino Urban Winery',
'USA',
'834 South County Line Rd.',
'Harpersfield',
'OH',
44041,
'216-973-2711',
'At Bene Vino, it’s all about the wine! Visit Bene Vino Urban Winery. You will find an award winning winery and wine without all the fussiness 
that goes with it. No wine snobs here, just fun people that enjoy fantastic wines, great appetizers from Benvenuto Grille and spectacular weekend 
entertainment. http://www.bennyvinourbanwinery.com/', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRILTU6pU4uiNtvFLiJLk_tlNIUqx5X-_S6LA&usqp=CAU', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Old Mill Winery',
'USA',
'403 S Broadway (St Rt 534)',
'Geneva',
'OH',
44041,
'440-466-5560',
'Built in the 1860''s, this historical mill retains the rustic charm of yesterday. All year long the Old Mill is the perfect place for friends and 
family to gather for great wine, great food and great times. Relax with a wine sangria, famous wineburger, delicious steak or pasta dinner.
Ask for Ohio Wines at your local grocery store and carryout. http://www.theoldmillwinery.com/', 'https://digitalmarketing.blob.core.windows.net/6207/images/items/image584380.jpg', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Silver Crest Cellars',
'USA',
'4511 Bates Rd',
'Madison',
'OH',
44057,
'440-299-7112',
'Silver Crest Cellars, located in the Grand River Valley,  is a collaboration of three winegrowers with local family vineyards dating back as far 
as the 1930’s.  We grow and make both old-world and new-world wine varieties including Riesling, Cabernet Franc, Cabernet Sauvignon, and Chardonnay, 
as well as local, historical varieties and blends. Ask for Ohio Wines at your local grocery store and carryout. https://silvercrestcellars.com/', 'https://images.squarespace-cdn.com/content/v1/58df041dbf629afc260de93a/1601599719860-80P48SGTNGXAY4PDE1B1/P1040928_DCE.JPG', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Urban Vintner',
'USA',
'37484 North Industrial Parkway',
'Willoughby',
'OH',
44094,
'440-487-1988',
'Urban Vintner was founded in 2016 by Ed Trebets and Gregg Turk. We started Urban Vintner because we wanted to bring back winemaking to the city where 
our forefathers started their winemaking traditions. In San Francisco, pre-the 1907 Earthquake, there were over 100 wineries in the city proper. After 
the destruction that took place, the wineries moved out to what is now Napa, Sonoma, and the other growing regions. The younger generation is bringing 
winemaking back to the city and there are now 25 urban wineries in the city of San Francisco. http://www.urbanvintner.net/', 'https://images.squarespace-cdn.com/content/v1/5b7efaff55b02cdb6c3f4174/1538361493775-0V8FJJWZCLBNHCIY0E5E/Urban+Vintner.jpg', 1);


--California
INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Acquiesce Winery',
'USA',
'22353 North Tretheway Road',
'Acampo',
'CA',
95220,
'209-333-6102',
'Love white wines? Acquiesce Winery located in the Lodi Appellation of California features estate award-winning white wines lovingly created in 
small batches. After selecting the best Rhône varieties, we carefully craft our unique wines which are bright and elegant fused with great fruit 
expression courtesy of Lodi’s sandy soils and Mediterranean climate. Join us for our Elevated Tasting Experience in our century old barn, surrounded 
by a hundred-acre view of wine grapes. *Lodi Rules for Sustainable Winegrowing Certified*', 'https://www.acquiescevineyards.com/wp-content/uploads/2020/07/acquiesce-main-nav-logo-02-01.jpg', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Twomey Cellars',
'USA',
'3000 Westside Road',
'Healdsburg',
'CA',
95448,
'707-942-7122',
'In 1999 Winemaker Daniel Baron and the Duncan Family created Twomey Cellars. It originated in a single Napa Valley vineyard; 
the Soda Canyon Ranch; where Merlot grows with such extraordinary complexity that it calls for a wine of its own. The wine is racked 
from barrel to barrel in the soutirage traditional. The marriage of classical French technique and an extraordinary California vineyard; 
results in Twomey’s distinctive; complex; and rich expression of luscious fruit.', 'https://twomey.com/wp-content/uploads/calistoga-carousel-lawn.jpg', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Cast Wines',
'USA',
'8500 Dry Creek Road',
'Geyserville',
'CA',
95441,
'707-431-1225',
'Tucked into the forest on a tranquil bench above our Grey Palm vineyard in beautiful Dry Creek Valley, Cast Wines grows and crafts wines in limited 
quantities highlighting the best grape sources of Sonoma County.', 'http://winecountry-media.s3.amazonaws.com/5168-media-Cast-Wines.jpg', 1);

INSERT INTO wineries(winery_name, winery_country, winery_address, winery_city, winery_state_abbr, winery_zip, winery_phone_number, description, image, status)
VALUES(
'Bear River Winery',
'USA',
'2751 Combie Road',
'Meadow Vista',
'CA',
95722,
'530-878-8959',
'Bear River Winery was founded in 2009 at the north end of Lake Combie on the banks of the Bear River in Meadow Vista. We are a family operated 
boutique micro wine grower producing small case lots of hand crafted wines using traditional methods from grapes sourced in and around Placer, 
Nevada and El Dorado county vineyards.', 'https://goodcheertrail.com/wp-content/uploads/2018/04/bear-river-1.jpg', 1);







--requests for users to become owners
INSERT INTO owner_request VALUES (9, 1001, 'please make me owner 1', 'pending');
INSERT INTO owner_request VALUES (9, 1001, 'please make me owner 2', 'pending');
INSERT INTO owner_request VALUES (9, 1001, 'please make me owner 3', 'pending');
INSERT INTO owner_request VALUES (9, 1001, 'please make me owner 4', 'pending');



--owners of the wineries
--INSERT INTO owners (owner_id, winery_id, request_id) VALUES ((SELECT user_id FROM users WHERE username = 'owner1'), (SELECT winery_id FROM wineries WHERE winery_name = 'Benny Vino Urban Winery'), (SELECT request_id FROM owner_request WHERE requester_id = (SELECT user_id FROM users WHERE username = 'owner1')));
INSERT INTO owners (owner_id, winery_id, request_id) VALUES ((SELECT user_id FROM users WHERE username = 'owner2'), (SELECT winery_id FROM wineries WHERE winery_name = 'Bear River Winery'), (SELECT request_id FROM owner_request WHERE requester_id = (SELECT user_id FROM users WHERE username = 'owner2')));

INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 1', (SELECT winery_id FROM wineries WHERE winery_name = 'North Coast Wine Club'), 'red', 'this is a dry red from winery 1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSNHy6suHjRQiP1wibvD6FDuEwBfL2gHMxF6g&usqp=CAU', 0);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 2', (SELECT winery_id FROM wineries WHERE winery_name = 'North Coast Wine Club'), 'red', 'this is a dry red from winery 1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQl2Rtunu6wEzflQn4DC0RymU_wYWShgG2-eA&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 3', (SELECT winery_id FROM wineries WHERE winery_name = 'North Coast Wine Club'), 'red', 'this is a dry red from winery 1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcREfOiLo0yiLn7Gj9C6sKP59c1alAKzsyt16g&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 4', (SELECT winery_id FROM wineries WHERE winery_name = 'North Coast Wine Club'), 'white', 'this is a dry white from winery 1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRjXGmOx_pfMasktzeOavhppL3vKmtxpncFHA&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 5', (SELECT winery_id FROM wineries WHERE winery_name = 'North Coast Wine Club'), 'white', 'this is a dry white from winery 1', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQN-APuidAoYVfShidcdoJJ6R7xalKla_plyQ&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 6', (SELECT winery_id FROM wineries WHERE winery_name = 'Autumn Rush Vinyard'), 'red', 'this is a dry red from winery 2', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQfY75OKNg6m51htNSpTpEZFv2JimyIFrPTcA&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 7', (SELECT winery_id FROM wineries WHERE winery_name = 'Autumn Rush Vinyard'), 'red', 'this is a dry red from winery 2', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT8UvEz3tMka-cjhU6n3Jn0qdC5AR3VOi_oPg&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 8', (SELECT winery_id FROM wineries WHERE winery_name = 'Autumn Rush Vinyard'), 'white', 'this is a dry white from winery 2', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSUj98rZxRMo38jHR2nOlD7AIxYptqrDDoAjg&usqp=CAU',1);
INSERT INTO wines(name, winery_id, style, description, image, status)
VALUES ('Wine 9', (SELECT winery_id FROM wineries WHERE winery_name = 'Autumn Rush Vinyard'), 'white', 'this is a dry white from winery 2', 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRK6dngFIYHzINk2SWCbgUkoQ5UpxB2Gh2E8A&usqp=CAU' ,1);


SELECT * FROM users;
SELECT * FROM wineries;
SELECT * FROM winery_reviews;
SELECT * FROM owner_request;
SELECT * FROM owners;
SELECT * FROM wines;
