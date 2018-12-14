create table SHours (
id_hour int not null PRIMARY KEY IDENTITY(0,1),
minutesStart int,
minutesEnd int,
timeStart varchar,
timeEnd varchar
);
create table Classrooms(
id_classroom int not null PRIMARY KEY IDENTITY(1,1),
nr int,
floor int
);
create table Lessons(
id_lesson int PRIMARY KEY NOT NULL IDENTITY(1,1),
name varchar(24)
);
create table DayOfSWeek(
id_day int not null PRIMARY KEY IDENTITY(1,1),
name varchar(16)
);
create table scheduler(
id_scheduler int PRIMARY KEY NOT NULL IDENTITY(1,1),
id_fDOW int foreign key references DayOfSWeek(id_day),
id_fLssn int foreign key references Lessons(id_lesson),
id_fH int foreign key references SHours(id_hour),
id_fClss int foreign key references Classrooms(id_classroom)
 
);
alter table Classrooms alter column nr varchar(5); 
alter table SHours alter column timeStart varchar(6);
alter table SHours alter column timeEnd varchar(6);
