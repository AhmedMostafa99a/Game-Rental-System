create database Game_Rent;

use Game_Rent;


create table users 
(
user_id int primary key,
userName varchar (70),
pass varchar (50),
email varchar (70),
address varchar(100)
);

create table client 
(
client_id int primary key,
clientName varchar (70),
pass varchar (50),
email varchar (70),
address varchar (100)
foreign key (client_id) references users (user_id)
);

create table admin 
(
admin_id int primary key,
adminName varchar (70),
pass varchar (50),
email varchar (70),
foreign key (admin_id) references users (user_id)
);

create table game 
(
game_id int primary key ,
gameName varchar (70),
[date] date ,
stock int,
);

create table category
(
category_id int primary key,
category_name varchar (70)
);

create table game_category (
    game_id int,
    category_id int,
    primary key (game_id, category_id),
    foreign key (game_id) references game(game_id),
    foreign key (category_id) references category(category_id)
);

create table vendor 
(
vendor_id int primary key,
vendName varchar (70),
phone varchar(11)

);

create table vendor_phone (
    vendor_id int,
    phone_num varchar (11),
    primary key (vendor_id, phone_num),
    foreign key (vendor_id) references vendor(vendor_id)
);

create table rent
(
client_id int, game_id int, 
rent_date date,
return_date date,
rent_cost decimal ,
primary key (client_id , game_id),
foreign key (client_id) references client(client_id),
foreign key (game_id) references game(game_id)
);

create table add_game 
(
game_id int , admin_id int,
primary key (game_id  , admin_id ),
foreign key (game_id) references game(game_id),
foreign key (admin_id) references admin (admin_id)

);

create table develop 
(
vendor_id int , game_id int,
primary key (vendor_id  , game_id ),
foreign key (game_id) references game(game_id),
foreign key (vendor_id) references vendor(vendor_id)
);
