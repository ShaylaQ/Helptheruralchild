CREATE USER [ST10269117@rcconnect.edu.za] FROM EXTERNAL PROVIDER;
ALTER ROLE db_owner ADD MEMBER [ST10269117@rcconnect.edu.za];


CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL DEFAULT 'Donor',
    Contact VARCHAR(50) NULL
);

CREATE TABLE Donations (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DonorId INT NOT NULL,
    Type VARCHAR(100) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    PaymentMethod VARCHAR(100) NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DonorId) REFERENCES Users(Id)
);

CREATE TABLE Pickups (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DonationId INT NOT NULL,
    DriverId INT NOT NULL,
    Location VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Scheduled',
    PickupDate DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DonationId) REFERENCES Donations(Id),
    FOREIGN KEY (DriverId) REFERENCES Users(Id)
);


CREATE TABLE TrackingPoints (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    DriverId INT NOT NULL,
    Latitude FLOAT NOT NULL,
    Longitude FLOAT NOT NULL,
    Timestamp DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (DriverId) REFERENCES Users(Id)
);


CREATE TABLE Messages (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ReceiverId INT NOT NULL,
    Sender VARCHAR(255) NOT NULL,
    Text VARCHAR(1000) NOT NULL,
    SentAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ReceiverId) REFERENCES Users(Id)
);




INSERT INTO Users (Name, Email, PasswordHash, Role) 
VALUES 
('Charity Coordinator', 'coordinator@ruralchild.org.za', 'coord!admin56', 'Admin'),
('Emily Carter', 'Emily.carter@ruralchild.org.za', 'EmC_Admin@01', 'Admin');


INSERT INTO Users (Name, Email, PasswordHash, Role) 
VALUES 
('Tshepiso Pinky', 'Precious@HTRC.com', 'Precious5', 'Donor'),
('Aiden Smith', 'aiden.smith@gmail.com', 'Aid3n#2025', 'Donor'),
('Lethabo Ndlovu', 'lethabo.ndlovu@gmail.com', 'Letha@0824', 'Donor'),
('Michael Daniels','mike.donates@gmail.com', 'MikeD!Giving88', 'Donor');


INSERT INTO Users (Name, Email, PasswordHash, Role, Contact)
VALUES
('Sipho Mokoena', 'sipho.driver@gmail.com', 'DriverPass1', 'Driver', '0556667777'),
('Thandi Khumalo', 'thandi.driver@gmail.com', 'DriverPass2', 'Driver', '0557778888'),
('Kabelo Maseko', 'kabelo.driver@gmail.com', 'DriverPass3', 'Driver', '0558889999');

SELECT * FROM Users;




