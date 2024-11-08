USE Xprience;

-- Users Table
CREATE TABLE Users (
    idUsers INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    username VARCHAR(50) NOT NULL,
    mail VARCHAR(50) NOT NULL,
    [password] VARCHAR(50) NOT NULL
);

-- Plan Table
CREATE TABLE [Plan] (
    idPlan INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
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
    FOREIGN KEY (idUsers) REFERENCES Users(idUsers)
);

-- PlanUser Table
CREATE TABLE PlanUser (
    idPlanUser INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idPlan INT NOT NULL,
    idUser INT NOT NULL,
    isFav BIT NULL,
    Created BIT NULL,
    FOREIGN KEY (idPlan) REFERENCES [Plan](idPlan),
    FOREIGN KEY (idUser) REFERENCES Users(idUsers)
);

-- Folder Table
CREATE TABLE Folder (
    idFolder INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    name VARCHAR(50) NOT NULL
);

-- FolderPlan Table
CREATE TABLE FolderPlan (
    idFolderUser INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    idFolder INT NOT NULL,
    idPlan INT NOT NULL,
    FOREIGN KEY (idFolder) REFERENCES Folder(idFolder),
    FOREIGN KEY (idPlan) REFERENCES [Plan](idPlan)
);
