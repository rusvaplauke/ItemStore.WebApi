CREATE TABLE purchases (
	userid INT,
	itemid INT,
	FOREIGN KEY (itemid) REFERENCES items(id)
);