CREATE DATABASE Xprience;

USE Xprience;

-- Users Table
CREATE TABLE Users (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    mail VARCHAR(50) NOT NULL,
    [password] VARCHAR(64) NOT NULL,
	cookie VARCHAR(36) NULL
);

-- Plan Table
CREATE TABLE [Plan] (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [name] VARCHAR(50) NOT NULL,
	[date] DATE NOT NULL,
    fsq_ids VARCHAR(MAX) NULL
);

-- Punctuation Table
CREATE TABLE Punctuation (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    fsq_id VARCHAR(24) NOT NULL,
    idUsers INT NOT NULL FOREIGN KEY REFERENCES Users(id) ON DELETE CASCADE,
    isFav BIT NULL,
    stars INT NULL,
    descripcion VARCHAR(256) NULL,
);

-- PlanUser Table
CREATE TABLE PlanUser (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idPlan INT NOT NULL FOREIGN KEY REFERENCES [Plan](id) ON DELETE CASCADE,
    idUser INT NOT NULL FOREIGN KEY REFERENCES Users(id) ON DELETE CASCADE,
    isFav BIT NULL,
    Created BIT NULL,
);

-- Folder Table
CREATE TABLE Folder (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [name] VARCHAR(50) NOT NULL
);

-- FolderPlan Table
CREATE TABLE FolderPlan (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idFolder INT NOT NULL FOREIGN KEY REFERENCES Folder(id) ON DELETE CASCADE,
    idPlan INT NOT NULL FOREIGN KEY REFERENCES [Plan](id) ON DELETE CASCADE,
);

GO;
CREATE PROCEDURE SignUp
	(@username VARCHAR(50), @mail VARCHAR(50), @password VARCHAR(64))
AS
BEGIN
	INSERT INTO Users (username, mail, [password]) VALUES (@username, @mail, @password);
	SELECT * FROM Users WHERE username = @username;
END;

GO;
CREATE PROCEDURE [LogIn]
	(@id INT)
AS
BEGIN
	DECLARE @cookie VARCHAR(36)
	SET @cookie = CONVERT(VARCHAR(36), NEWID());
	UPDATE Users SET cookie = @cookie WHERE id = @id;
	SELECT cookie FROM Users WHERE cookie = @cookie;
END;

GO;
CREATE PROCEDURE GeneratePlan
	(@fsq_ids VARCHAR(MAX), @day INT, @month INT, @year INT, @userId INT)
AS
BEGIN
	SET @month = @month + 1;
	INSERT INTO [Plan]([name], [date], fsq_ids) VALUES ('My plan', DATEFROMPARTS(@year, @month, @day), @fsq_ids);
	DECLARE @idPlan INT;
	SET @idPlan = (SELECT TOP 1 id FROM [Plan] ORDER BY id DESC);
	INSERT INTO [PlanUser](idPlan, idUser, Created) VALUES (@idPlan, @userId, 1);
END;

GO;
CREATE PROCEDURE GetFolder
	(@idFolder INT, @idUser INT)
AS
BEGIN
	IF (@idFolder = -1)
	BEGIN
		SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @idUser AND PlanUser.Created = 1
	END;
END;

SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = 3 AND PlanUser.Created = 1