--Creating DB

CREATE DATABASE MinionsDB

USE MinionsDB

CREATE TABLE Countries(
Id INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50)
);

CREATE TABLE Towns(
Id INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50),
CountryCode INT,
CONSTRAINT fk_towns_countries
FOREIGN KEY(CountryCode)
REFERENCES Countries(Id)
);
CREATE TABLE Minions(
Id INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50),
Age INT,
TownID INT,
CONSTRAINT fk_minions_towns
FOREIGN KEY(TownID)
REFERENCES Towns(Id)
);
CREATE TABLE EvilnessFactors(
Id INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50)
);
CREATE TABLE Villains(
Id INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50),
EvilnessFactorId INT,
CONSTRAINT fk_villains_evilnessfactor
FOREIGN KEY (EvilnessFactorId)
REFERENCES EvilnessFactors(Id)
);
CREATE TABLE MinionVillains(
MinionId INT,
VillainId INT,
PRIMARY KEY (MinionId, VillainId),
CONSTRAINT fk_minions
FOREIGN KEY(MinionId)
REFERENCES Minions(Id),
CONSTRAINT fk_villains
FOREIGN KEY(VillainId)
REFERENCES Villains(Id)
);
