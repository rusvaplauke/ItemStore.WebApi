CREATE TABLE shop_items (
	shop_id INT,
	item_id INT,
	FOREIGN KEY (shop_id) REFERENCES shops(id),
	FOREIGN KEY (item_id) REFERENCES items(id)
);