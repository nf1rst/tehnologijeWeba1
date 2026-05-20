-- =========================
-- COUNTRIES
-- =========================
INSERT INTO Countries (Name) VALUES
('Germany'),
('Sweden'),
('USA');

-- =========================
-- BANDS
-- =========================
INSERT INTO Bands (Name, Status, YearOfFormation, CountryId) VALUES
('Rammstein', 'Active', 1994, 1),
('Ghost', 'Active', 2006, 2),
('System of a Down', 'Active', 1994, 3),
('Death', 'Inactive', 1983, 3);

-- =========================
-- PERSONS (MEMBERS)
-- =========================
INSERT INTO Persons (Fname, Lname, DateOfBirth, Sex, ArtistName, PlaceOfBirthId) VALUES
-- Rammstein
('Till', 'Lindemann', '1963-01-04', 'M', NULL, 1),
('Richard', 'Kruspe', '1967-06-24', 'M', NULL, 1),
('Paul', 'Landers', '1964-12-09', 'M', NULL, 1),
('Christoph', 'Schneider', '1966-05-11', 'M', NULL, 1),
('Oliver', 'Riedel', '1971-04-11', 'M', NULL, 1),
('Christian', 'Lorenz', '1966-11-16', 'M', 'Flake', 1),

-- Ghost
('Tobias', 'Forge', '1981-03-03', 'M', NULL, 2),

-- SOAD
('Serj', 'Tankian', '1967-08-21', 'M', NULL, 3),
('Daron', 'Malakian', '1975-07-18', 'M', NULL, 3),
('Shavo', 'Odadjian', '1974-04-22', 'M', NULL, 3),
('John', 'Dolmayan', '1973-07-15', 'M', NULL, 3),

-- Death
('Chuck', 'Schuldiner', '1967-05-13', 'M', NULL, 3);

-- =========================
-- BANDPERSON (M:N)
-- =========================

-- Rammstein (BandId = 1)
INSERT INTO BandPersons (BandId, PersonId, Status, RoleInBand) VALUES
(1, 1, 0, 'Vocals'),
(1, 2, 0, 'Lead Guitar'),
(1, 3, 0, 'Rhythm Guitar'),
(1, 4, 0, 'Drums'),
(1, 5, 0, 'Bass'),
(1, 6, 0, 'Keyboard');

-- Ghost (BandId = 2)
INSERT INTO BandPersons (BandId, PersonId, Status, RoleInBand) VALUES
(2, 7, 0, 'Vocals');

-- System of a Down (BandId = 3)
INSERT INTO BandPersons (BandId, PersonId, Status, RoleInBand) VALUES
(3, 8, 0, 'Vocals'),
(3, 9, 0, 'Guitar'),
(3, 10, 0, 'Bass'),
(3, 11, 0, 'Drums');

-- Death (BandId = 4)
INSERT INTO BandPersons (BandId, PersonId, Status, RoleInBand) VALUES
(4, 12, 1, 'Vocals/Guitar');