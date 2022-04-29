SELECT * FROM users;
SELECT * FROM wineries;
SELECT * FROM venues;
SELECT * FROM venue_reviews;
SELECT * FROM owner_request;
SELECT * FROM owners;
SELECT * FROM wines;


INSERT INTO wines (name, winery_id, style, description, image) VALUES (' dd', 2008, 'red', 'sadf', '');


UPDATE wines SET name = @name, style = @style, description = @description, image = @image WHERE wine_id = @wine_id;


SELECT * FROM wines WHERE wine_id = 10011;