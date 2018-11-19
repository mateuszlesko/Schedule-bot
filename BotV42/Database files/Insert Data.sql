
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(430,475,'07:10','07:55');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(480,525,'08:00','08:45');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(530,575,'08:50','09:35');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(585,630,'09:45','10:30');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(640,685,'10:40','11:25');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(700,745,'11:40','12:25');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(765,810,'12:45','13:30');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(825,870,'13:45','14:30');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(880,925,'14:40','15:25');
insert into SHours (minutesStart,minutesEnd,timeStart,timeEnd) values(930,975,'15:30','16:20');


insert into Lessons(name) values ('Polish');
insert into Lessons(name) values ('Maths');
insert into Lessons(name) values ('Natural Sciences');
insert into Lessons(name) values ('Art');
insert into Lessons(name) values ('Music');
insert into Lessons(name) values ('Physical Education');
insert into Lessons(name) values ('English');
insert into Lessons(name) values ('Religion Education');
insert into Lessons(name) values ('Technology');
insert into Lessons(name) values ('History');
insert into Lessons(name) values ('Information Technology');
insert into Lessons(name) values ('wimming pool');
insert into Lessons(name) values ('ice rink');


insert into DayOfSWeek(name) values('Monday');
insert into DayOfSWeek(name) values('Tuesday');
insert into DayOfSWeek(name) values('Wednesday');
insert into DayOfSWeek(name) values('Thursday');
insert into DayOfSWeek(name) values('Friday');

Insert into Classrooms(nr,floor) values ('gym',0);
Insert into Classrooms(nr,floor) values ('33',1);
Insert into Classrooms(nr,floor) values ('36',1);
Insert into Classrooms(nr,floor) values ('24',1);
Insert into Classrooms(nr,floor) values ('35',1);
Insert into Classrooms(nr,floor) values ('31',1);
Insert into Classrooms(nr,floor) values ('25',1);
Insert into Classrooms(nr,floor) values ('12',0);
Insert into Classrooms(nr,floor) values ('23',1);
Insert into Classrooms(nr,floor) values ('11',0);
Insert into Classrooms(nr,floor) values ('22',1);


/* for monday */
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,3,6,1);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,4,10,2);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,5,9,12);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,6,7,4);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,7,2,5);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(1,8,5,12);

/* for tuesday */
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,2,2,5);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,3,1,6);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,4,1,6);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,5,7,10);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,6,3,7);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(2,7,6,1);

/* for wednesday*/
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(3,5,15,13);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(3,6,1,6);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(3,7,2,5);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (3,8,8,8);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (3,9,4,3);
/*for thursday*/
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(4,2,17,6);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(4,3,14,9);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(4,4,6,1);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (4,5,1,6);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values(4,7,11,11);

/*for friday */

insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (5,5,14,1);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (5,6,2,5);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (5,7,8,8);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (5,8,7,10);
insert into scheduler (id_fDOW,id_fH,id_fLssn,id_fClss) values (5,9,3,7);

