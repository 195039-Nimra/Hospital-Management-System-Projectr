CREATE TABLE [dbo].[Patienttbl]
(
	[Patid] INT NOT NULL PRIMARY KEY, 
    [PatName] VARCHAR(50) NOT NULL, 
    [PatAddress] VARCHAR(50) NOT NULL, 
    [PatPhone] VARCHAR(50) NOT NULL, 
    [PatAge] INT NOT NULL, 
    [PatGender] VARCHAR(50) NOT NULL, 
    [PatBlood] VARCHAR(50) NOT NULL, 
    [PatDisease] VARCHAR(50) NOT NULL
)
