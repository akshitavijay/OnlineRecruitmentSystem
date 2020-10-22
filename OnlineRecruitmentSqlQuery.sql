create database OnlineRecruitment;

use OnlineRecruitment;

create table Role( roleId int primary key, roleName varchar(20));


create table Users( 
userId int primary key identity(101, 1),
password varchar(20) not null,
roleId int references role(roleId), 
name varchar(20) not null,
email varchar(30) not null,
phone varchar(10) not null,
gender char(1)
)


create table JobSeeker
(
jobSeekerID int primary key identity(101,1),
userId int references Users(userId),
address varchar(40) ,
experience int,
qualification varchar(50),
profession varchar(20),
skillset varchar(40),
resume nvarchar(MAX),

)


create table Employer(
jobID int identity(150,1) primary key,
userId int references Users(userId),
compName varchar(30) not null,
jobCategory varchar(40) not null,
jobTitle varchar(40) not null,
currentOpenings int ,
joiningDate date,
location varchar(50),
designation varchar(20),
experience int,
qualification varchar(50),
skillSet varchar(40),
salary money,
contactDetails varchar(40)
)



