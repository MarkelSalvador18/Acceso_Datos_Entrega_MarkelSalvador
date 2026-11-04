-- ============================================================
-- Datos de ejemplo para la base de datos 4vGYM (SQL Server)
-- Tablas (generadas por la migracion InitialCreate7):
--   ActivityTypes, Intructors, Activities, ActivityInstructor
-- Ejecutar DESPUES de aplicar las migraciones (Update-Database).
-- ============================================================

USE [4vGYM];
GO

-- ------------------------------------------------------------
-- Limpieza (orden inverso por las claves foraneas)
-- ------------------------------------------------------------
DELETE FROM ActivityInstructor;
DELETE FROM Activities;
DELETE FROM Intructors;
DELETE FROM ActivityTypes;
GO

DBCC CHECKIDENT ('Activities', RESEED, 0);
DBCC CHECKIDENT ('Intructors', RESEED, 0);
DBCC CHECKIDENT ('ActivityTypes', RESEED, 0);
GO

-- ------------------------------------------------------------
-- ActivityTypes (Id, Name, NumberMonitors)
-- ------------------------------------------------------------
SET IDENTITY_INSERT ActivityTypes ON;
INSERT INTO ActivityTypes (Id, Name, NumberMonitors) VALUES
(1, N'Spinning', 1),
(2, N'Yoga',     1),
(3, N'Crossfit', 2),
(4, N'Pilates',  1),
(5, N'Zumba',    2);
SET IDENTITY_INSERT ActivityTypes OFF;
GO

-- ------------------------------------------------------------
-- Intructors (Id, Name, Email, PhotoUrl, Phone)
-- ------------------------------------------------------------
SET IDENTITY_INSERT Intructors ON;
INSERT INTO Intructors (Id, Name, Email, PhotoUrl, Phone) VALUES
(1, N'Laura Martinez', N'laura.martinez@4vgym.com', N'https://example.com/photos/laura.jpg',  N'600111222'),
(2, N'Carlos Gomez',   N'carlos.gomez@4vgym.com',   N'https://example.com/photos/carlos.jpg', N'600333444'),
(3, N'Ana Ruiz',       N'ana.ruiz@4vgym.com',       N'https://example.com/photos/ana.jpg',    N'600555666'),
(4, N'Pablo Sanchez',  N'pablo.sanchez@4vgym.com',  N'https://example.com/photos/pablo.jpg',  N'600777888'),
(5, N'Marta Lopez',    N'marta.lopez@4vgym.com',    N'https://example.com/photos/marta.jpg',  N'600999000');
SET IDENTITY_INSERT Intructors OFF;
GO

-- ------------------------------------------------------------
-- Activities (Id, MaxParticipants, DateStart, DateEnd, ActivityTypeId)
-- ------------------------------------------------------------
SET IDENTITY_INSERT Activities ON;
INSERT INTO Activities (Id, MaxParticipants, DateStart, DateEnd, ActivityTypeId) VALUES
(1, 20, '2026-11-10T09:00:00', '2026-11-10T10:00:00', 1), -- Spinning
(2, 15, '2026-11-10T18:00:00', '2026-11-10T19:00:00', 2), -- Yoga
(3, 12, '2026-11-11T07:30:00', '2026-11-11T08:30:00', 3), -- Crossfit
(4, 18, '2026-11-12T17:00:00', '2026-11-12T18:00:00', 4), -- Pilates
(5, 25, '2026-11-13T19:00:00', '2026-11-13T20:00:00', 5), -- Zumba
(6, 20, '2026-11-14T10:00:00', '2026-11-14T11:00:00', 1); -- Spinning
SET IDENTITY_INSERT Activities OFF;
GO

-- ------------------------------------------------------------
-- ActivityInstructor (ActivitiesId, InstructorsId)
-- Relacion muchos a muchos entre Activities e Intructors.
-- Respeta NumberMonitors de cada tipo de actividad.
-- ------------------------------------------------------------
INSERT INTO ActivityInstructor (ActivitiesId, InstructorsId) VALUES
(1, 1),         -- Spinning: 1 monitor
(2, 3),         -- Yoga: 1 monitor
(3, 2), (3, 4), -- Crossfit: 2 monitores
(4, 5),         -- Pilates: 1 monitor
(5, 1), (5, 5), -- Zumba: 2 monitores
(6, 2);         -- Spinning: 1 monitor
GO

-- Comprobacion
SELECT a.Id, t.Name AS Tipo, a.MaxParticipants, a.DateStart, a.DateEnd, i.Name AS Monitor
FROM Activities a
JOIN ActivityTypes t ON t.Id = a.ActivityTypeId
JOIN ActivityInstructor ai ON ai.ActivitiesId = a.Id
JOIN Intructors i ON i.Id = ai.InstructorsId
ORDER BY a.Id;
GO
