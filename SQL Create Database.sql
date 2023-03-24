Create database Demo

use Demo


create table Destinations(
	IDdes int Primary key,
	nameDes VARCHAR(50),
	Distance float,
	Transport VARCHAR(50),
	Price money
)

create table CreditCard(
	IDcredit VARCHAR(10) Primary key,
	NameUser VARCHAR(50),
	expDate VARCHAR(5),
	CVV INT 

)

INSERT INTO Destinations(IDdes, nameDes, Distance, Transport, Price)
VALUES
(1, 'Paris', 12.5, 'Bus',500.00),
(2, 'American', 19.7, 'Bus',800.00),
(3, 'Idia', 13.50, 'Bus',550.00),
(4, 'France', 21.50, 'Bus',890.00),
(5, 'Paris', 12.50, 'Train',600.00),
(6, 'American', 19.7, 'Train',700.00),
(7, 'Idia',  13.50, 'Train',650.00),
(8, 'France', 21.50, 'Train',999.00),
(9, 'Paris', 12.50, 'Test1',200.00),
(10, 'American', 19.7, 'Test1',300.00),
(11, 'Idia',  13.50, 'Test1',350.00),
(12, 'France', 21.50, 'Test1',590.00),
(13, 'Paris', 12.50, 'Test2',1500.00),
(14, 'American', 19.7, 'Test2',1100.00),
(15, 'Idia', 13.50, 'Test2',1300.00),
(16, 'France', 21.50, 'Test2',1890.00);


INSERT INTO CreditCard(IDcredit, NameUser, expDate, CVV)
VALUES
('1111111111', 'John Doe', '12/25', 123),
('1111111119', 'Jane Smith', '05/24', 456),
('1111111122', 'Robert Johnson', '08/23', 789),
('1111112224', 'Sarah Lee', '11/26', 321),
('1234567891', 'David Kim', '02/25', 654);
