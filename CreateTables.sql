CREATE TABLE Cinemas (
    CinemaID INT AUTO_INCREMENT,
    CinemaName VARCHAR(255),
    NumberOfHalls INT,
    NumberOfSeats INT,
    PRIMARY KEY (CinemaID)
);

CREATE TABLE Billboard (
    BillboardID INT AUTO_INCREMENT,
    CinemaID INT,
    MovieID INT,
    ShowTime DATETIME,
    TicketPrice DECIMAL(5,2),
    NumberOfTickets INT,
    PRIMARY KEY (BillboardID)
);

CREATE TABLE Movies (
    MovieID INT AUTO_INCREMENT,
    MovieName VARCHAR(255),
    PRIMARY KEY (MovieID)
);

INSERT INTO Cinemas (CinemaName, NumberOfHalls, NumberOfSeats)
VALUES 
('Cinema1', 5, 500),
('Cinema2', 4, 400),
('Cinema3', 6, 600),
('Cinema4', 7, 700),
('Cinema5', 8, 800),
('Cinema6', 9, 900),
('Cinema7', 10, 1000),
('Cinema8', 11, 1100),
('Cinema9', 12, 1200),
('Cinema10', 13, 1300);

INSERT INTO Billboard (CinemaID, MovieID, ShowTime, TicketPrice, NumberOfTickets)
VALUES 
(1, 1, '2024-01-01 10:00:00', 10.00, 100),
(2, 2, '2024-01-02 11:00:00', 20.00, 200),
(3, 3, '2024-01-03 12:00:00', 30.00, 300),
(4, 4, '2024-01-04 13:00:00', 40.00, 400),
(5, 5, '2024-01-05 14:00:00', 50.00, 500),
(6, 6, '2024-01-06 15:00:00', 60.00, 600),
(7, 7, '2024-01-07 16:00:00', 70.00, 700),
(8, 8, '2024-01-08 17:00:00', 80.00, 800),
(9, 9, '2024-01-09 18:00:00', 90.00, 900),
(10, 10, '2024-01-10 19:00:00', 100.00, 1000);

INSERT INTO Movies (MovieName)
VALUES 
('Movie1'),
('Movie2'),
('Movie3'),
('Movie4'),
('Movie5'),
('Movie6'),
('Movie7'),
('Movie8'),
('Movie9'),
('Movie10');

ALTER TABLE Movies
ADD COLUMN Genre VARCHAR(255),
ADD COLUMN Duration INT;

