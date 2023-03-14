use[master];
go
if DB_ID('Monopoly2')is not null
begin 
drop database [Monopoly2];
end
go
create database [Monopoly2]
go
use [Monopoly2];
go
CREATE TABLE [Players]
(
	[Id] int not null identity(1,1) primary key,
	[Name] varchar(30) not null,
	[Password] int not null,
	[Balance] money not null,
	[AmountCards] int not null,
	[Vip] bit not null
);
go
CREATE TABLE [Cards]
(
	[Id] int not null identity(1,1) primary key,
	[Name] varchar(30) not null,
	[Price] money not null,
	[Class] nvarchar(30) not null,
	[Mono] nvarchar(30) not null,
	[Image] IMAGE not null
);
go
CREATE TABLE [Market]--таблица продажа карточки ссылается на маркет
(
	[Id] int not null identity(1,1) primary key,
	[IdCard] int not null ,
	[CountCard] int not null,
	[PriceCard] money not null,
)
go 
CREATE TABLE [Sales]
(
	[Id] int not null identity(1,1)primary key,
	[IdCard] int not null unique,
	[CountCard] int not null,
	[PriceCard] money not null,
	[DateTime] datetime not null,
)