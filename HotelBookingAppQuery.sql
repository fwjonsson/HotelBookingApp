SELECT * 
FROM dbo.Customers
ORDER BY BirthDate


SELECT 
	c.Id,
	c.FullName,
	COUNT(b.Id) AS NumberOfBookings
FROM Customers c
INNER JOIN Bookings b ON c.Id = b.CustomerId
GROUP BY 
	c.Id,
	c.FullName
	ORDER BY NumberOfBookings DESC, c.FullName ASC;


SELECT *
FROM dbo.Customers
WHERE BirthDate <= DATEADD (YEAR, -65 , CAST(GETDATE() AS date));


SELECT * 
FROM dbo.Rooms
WHERE RoomType = 2
ORDER BY RoomNumber;

SELECT *
FROM dbo.Rooms
ORDER BY RoomSizeSqm2;


SELECT * 
FROM Bookings
ORDER BY StartDate;


SELECT *,
	DATEDIFF(DAY, StartDate, EndDate) AS BookingLength
FROM Bookings
ORDER BY BookingLength DESC;


