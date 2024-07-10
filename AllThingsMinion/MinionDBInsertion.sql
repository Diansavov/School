--Insterion
USE MinionsDB

INSERT INTO Countries(Name)
VALUES
('Bulgaria'),
('Germany'),
('Japan'),
('Italy'),
('Hungary');

INSERT INTO Towns(Name, CountryCode)
VALUES
('Sofia', 1),
('Berlin', 2),
('Tokio', 3),
('Rome', 4),
('Budapest', 5);

INSERT INTO Minions(Name, Age, TownID)
VALUES
('Kat', 20, 2),
('Penio', 21, 1),
('Kohaku', 16, 3),
('Bela', 35, 5),
('Beatrice', 16, 4),
('Johan', 18, 2),
('Heinz', 18, 2),
('Heinrich', 18, 2),
('Ella', 18, 2),
('Linna', 18, 2);
INSERT INTO EvilnessFactors(Name)
VALUES
('Super Good'),
('Good'),
('Bad'),
('SuperBad'),
('PureEvil');
INSERT INTO Villains(Name, EvilnessFactorId)
VALUES
('Napoleon', 1),
('Gru', 3),
('Vector', 4),
('Adolf', 1),
('Pena', 5);

INSERT INTO MinionVillains(MinionId, VillainId)
VALUES
(1, 4),
(2, 5),
(3, 1),
(4, 2),
(5, 3),
(6, 4),
(7, 4),
(8, 4),
(9, 4),
(6, 1),
(7, 1);
