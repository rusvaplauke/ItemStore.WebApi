CREATE TABLE shop_items (
	shopid INT,
	itemid INT,
	FOREIGN KEY (shopid) REFERENCES shops(id),
	FOREIGN KEY (itemid) REFERENCES items(id)
);