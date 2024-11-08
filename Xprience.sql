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
    name VARCHAR(50) NOT NULL,
    text VARCHAR(MAX) NOT NULL,
    fsq_ids VARCHAR(MAX) NULL
);

-- Punctuation Table
CREATE TABLE Punctuation (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    fsq_id VARCHAR(24) NOT NULL,
    idUsers INT NOT NULL,
    isFav BIT NULL,
    stars INT NULL,
    descripcion VARCHAR(256) NULL,
    FOREIGN KEY (id) REFERENCES Users(id)
);

-- PlanUser Table
CREATE TABLE PlanUser (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idPlan INT NOT NULL,
    idUser INT NOT NULL,
    isFav BIT NULL,
    Created BIT NULL,
    FOREIGN KEY (id) REFERENCES [Plan](id),
    FOREIGN KEY (id) REFERENCES Users(id)
);

-- Folder Table
CREATE TABLE Folder (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(50) NOT NULL
);

-- FolderPlan Table
CREATE TABLE FolderPlan (
    id INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idFolder INT NOT NULL,
    idPlan INT NOT NULL,
    FOREIGN KEY (id) REFERENCES Folder(id),
    FOREIGN KEY (id) REFERENCES [Plan](id)
);

GO;
CREATE PROCEDURE SignUp
	(@username VARCHAR(50), @mail VARCHAR(50), @password VARCHAR(50))
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
	SELECT cookie FROM Users WHERE cookie = @cookie
END;

EXEC SignUp 'eldisnex', 'si', '123'
EXEC [LogIn] 1