use University

create table Student(
	Id int identity(1,1) constraint PK_Student primary key,
	Name nvarchar(100),
	Age int
)

create table Groups(
	Id int identity(1,1) constraint PK_Groups primary key,
	Name nvarchar(100),
)

create table StudentInGroup(
	StudentId int constraint PK_Post_Student references Student(Id),
	GroupsId int constraint PK_Post_Groups references Groups(Id)
)

select * from Student
select * from Groups
select * from StudentInGroup