create database animalshelter;
use animalshelter;
create table animals (name varchar(255), sex enum('Male','Female'), date_of_admittance datetime, breed varchar(255), type varchar(255));

insert into animals (name, sex, date_of_admittance, breed, type)
values ('Rex', 'Male', '2019-01-01', 'Collie', 'Dog');
insert into animals (name, sex, date_of_admittance, breed, type)
values ('Kittie', 'Female', '2019-02-26', 'Minx', 'Cat');
insert into animals (name, sex, date_of_admittance, breed, type)
values ('Billy', 'Male', '2019-01-15', 'Angora', 'Goat');


create database animalshelter_test;
use animalshelter_test;
create table animals (name varchar(255), sex enum('male','female'), date_of_admittance datetime, breed varchar(255), type varchar(255));
