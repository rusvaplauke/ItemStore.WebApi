
CREATE TABLE user_items(
	user_id INT,
	item_id INT,
	FOREIGN KEY (item_id) REFERENCES items(id)
);