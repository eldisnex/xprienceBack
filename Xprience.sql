CREATE DATABASE Xprience;

USE Xprience;

-- Users Table
CREATE TABLE Users (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    mail VARCHAR(50) NOT NULL,
    [password] VARCHAR(64) NOT NULL,
	cookie VARCHAR(36) NULL,
	[language] VARCHAR(2) NULL DEFAULT 'es'
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
    idPlan INT NOT NULL FOREIGN KEY REFERENCES [PlanUser](id) ON DELETE CASCADE,
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
		SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @idUser AND PlanUser.Created = 1;		
	END;
	ELSE IF (@idFolder = 0)
	BEGIN
		SELECT [Plan].* FROM [Plan] INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan INNER JOIN Users ON PlanUser.idUser = Users.id WHERE Users.id = @idUser AND PlanUser.isFav = 1;
	END;
	ELSE
	BEGIN
		SELECT [Plan].* FROM [Plan]
		INNER JOIN PlanUser ON [Plan].id = PlanUser.idPlan
		INNER JOIN Users ON PlanUser.idUser = Users.id
		INNER JOIN FolderPlan ON FolderPlan.idPlan = [Plan].id
		INNER JOIN Folder ON Folder.id = FolderPlan.idFolder
		WHERE Users.id = @idUser AND Folder.id = @idFolder
		GROUP BY [Plan].id, [Plan].[name], [Plan].[date], [Plan].fsq_ids;
	END;
END;

GO;
CREATE PROCEDURE LikePlan
	(@idUser INT, @idPlan INT)
AS
BEGIN
	IF EXISTS (SELECT id FROM PlanUser WHERE idPlan = @idPlan AND idUser = @idUser)
	BEGIN
		UPDATE PlanUser SET isFav = 1 WHERE idPlan = @idPlan AND idUser = @idUser;
	END;
	ELSE
	BEGIN
		INSERT INTO PlanUser(idUser, idPlan, isFav) VALUES (@idUser, @idPlan, 1);
	END;
END;

GO;
CREATE PROCEDURE LikePlace
	(@idUser INT, @fsq_id VARCHAR(24))
AS
BEGIN
	IF EXISTS (SELECT id FROM Punctuation WHERE fsq_id = @fsq_id AND idUsers = @idUser)
	BEGIN
		UPDATE Punctuation SET isFav = 1 - isFav WHERE fsq_id = @fsq_id AND idUsers = @idUser;
	END;
	ELSE
	BEGIN
		INSERT INTO Punctuation(fsq_id, idUsers, isFav) VALUES (@fsq_id, @idUser, 1);
	END;
END;

GO;
CREATE PROCEDURE AddFolder
	(@idUser INT, @idPlan INT, @name VARCHAR(50))
AS
BEGIN
	INSERT INTO Folder([name]) VALUES (@name);
	INSERT INTO FolderPlan(idFolder, idPlan) VALUES ((SELECT TOP 1 id FROM Folder ORDER BY id DESC), (SELECT id FROM PlanUser WHERE idPlan = @idPlan AND idUser = @idUser)); 
END;

GO;
CREATE PROCEDURE AddToFolder
	(@idFolder INT, @idUser INT, @idPlan INT)
AS
BEGIN
	INSERT INTO FolderPlan(idFolder, idPlan) VALUES (@idFolder, (SELECT id FROM PlanUser WHERE idPlan = @idPlan AND idUser = @idUser)); 
END;