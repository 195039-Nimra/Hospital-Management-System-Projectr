CREATE TABLE [dbo].[Diagnosistbl]
(
	[DiagId] INT NOT NULL PRIMARY KEY, 
    [PatId] INT NOT NULL, 
    [PatName] VARCHAR(50) NOT NULL, 
    [Symptoms] VARCHAR(50) NOT NULL, 
    [Diagnosis] VARCHAR(50) NOT NULL, 
    [Medicines] VARCHAR(50) NOT NULL
)
