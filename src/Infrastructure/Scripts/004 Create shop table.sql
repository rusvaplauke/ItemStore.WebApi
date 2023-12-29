CREATE TABLE shops (
	id serial PRIMARY KEY,
	name VARCHAR(50),
	address VARCHAR(200),
	is_deleted BOOLEAN DEFAULT FALSE
);
