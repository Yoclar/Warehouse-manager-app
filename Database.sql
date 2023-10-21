Create Database warehouse
Default Character Set utf8
Collate utf8_hungarian_ci;
/* Categories */
CREATE TABLE categories(
id int(11) PRIMARY KEY AUTO_INCREMENT not null,
name varchar(100) not null
);
INSERT INTO `categories`( `name`) VALUES ('electronics'),('outdoors'),('cooking'),('furniture');
/* Items */
CREATE TABLE items(
id int(11) PRIMARY KEY not null AUTO_INCREMENT,
categoryid int(11) ,
name varchar(100) not null,
description varchar(255),
brand varchar(100),
quantity int(100) not null,
FOREIGN KEY (categoryid) REFERENCES categories(id)
);
INSERT INTO `items`( `categoryid`, `name`, `description`, `brand`, `quantity`) VALUES ('1','Laptop','We use it in our everyday life','Dell','1');
/* Locations */
CREATE TABLE locations(
id int(11) PRIMARY KEY not null AUTO_INCREMENT,
name varchar(50) not null,
capacity int(200) not null
);
INSERT INTO `locations`( `name`, `capacity`) VALUES ('shelf','10');
/* Item -> Loctem <- Locations */
CREATE TABLE loctem(
id int(11) PRIMARY KEY not null AUTO_INCREMENT,
itemid int(11),
locationid int(11),
FOREIGN KEY (itemid) REFERENCES items(id),
FOREIGN KEY (locationid) REFERENCES locations(id)
);
INSERT INTO `loctem`(`itemid`, `locationid`) VALUES (1,1);
/* Users */
CREATE TABLE users(
id int(11) PRIMARY KEY not null AUTO_INCREMENT,
employee tinyint not null,
name varchar(255) not null,
locationid int(11),
FOREIGN KEY (locationid) REFERENCES locations(id)
);
INSERT INTO `users`(`employee`,`name`, `locationid`) VALUES ('1','Gazsi','1');