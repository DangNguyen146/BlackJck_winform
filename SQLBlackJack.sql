CREATE DATABASE Client
GO
USE Client
GO
CREATE TABLE CLIENT(ID INT NOT NULL,IP CHAR(15) NOT NULL,NUMOFCARD INT,	SUMCARD INT,CONSTRAINT PK_KH PRIMARY KEY(ID))

SELECT * FROM CLIENT 
insert into CLIENT (ID, IP) VALUES(3, '192.168.1.3')
update CLIENT SET NUMOFCARD=2 WHERE IP='192.163.1.3'
update CLIENT SET SUMCARD=2 WHERE IP='192.163.1.3'
DROP TABLE CLIENT
SELECT COUNT(IP) FROM CLIENT

delete from Client

insert into CLIENT (ID, IP) VALUES(1, '127.0.0.1:62630')

select top 1 IP from CLIENT order by IP desc;			--select hang cuoi

SELECT NUMOFCARD FROM Client WHERE IP='127.0.0.1:60618'
SELECT SUMCARD FROM Client WHERE IP='127.0.0.1:60618'

ALTER TABLE CLIENT ADD DAN INT


SELECT COUNT(*) FROM Client WHERE DAN=1