CREATE DATABASE cs4471Project;
USE cs4471Project;
CREATE TABLE customers(
  customer_id VARCHAR(20) NOT NULL PRIMARY KEY
  , customer_name VARCHAR(255) NOT NULL
  , balance DECIMAL(10, 2) NOT NULL
  , amount_paid DECIMAL(10, 2) NOT NULL
);

CREATE TABLE items(
  item_id VARCHAR(20) NOT NULL AUTO INCREMENT PRIMARY KEY
  , item_name VARCHAR(255) NOT NULL
  , cost DECIMAL(10, 2) NOT NULL
  , stock INT NOT NULL
);

CREATE TABLE rents(
  customer_id VARCHAR(20) NOT NULL
  , item_id VARCHAR(20) NOT NULL
  , rent_cost DECIMAL(10, 2) NOT NULL
  , date_rented DATE NOT NULL
  , CONSTRAINT PK_rents PRIMARY KEY (customer_id, item_id)
  , FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
  , FOREIGN KEY (item_id) REFERENCES items(item_id)
);