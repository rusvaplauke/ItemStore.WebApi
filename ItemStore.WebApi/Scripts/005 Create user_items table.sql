CREATE TABLE shops (
	id serial PRIMARY KEY,
	name VARCHAR(50),
	address VARCHAR(200),
	is_deleted BOOLEAN DEFAULT FALSE
);

CREATE TABLE shop_items(
	shop_id INT,
	item_id INT,
	FOREIGN KEY (shop_id) REFERENCES shops(id),
	FOREIGN KEY (item_id) REFERENCES items(id)
);