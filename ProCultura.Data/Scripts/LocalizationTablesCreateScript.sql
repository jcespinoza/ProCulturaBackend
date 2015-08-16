----Create Schema
IF NOT EXISTS (
	SELECT
		SCHEMA_NAME
	FROM INFORMATION_SCHEMA.SCHEMATA
	WHERE SCHEMA_NAME = 'Localization'
)
BEGIN
	EXEC sp_executesql N'CREATE SCHEMA Localization'
END

BEGIN
	IF OBJECT_ID('Localization.Dictionary', 'U') IS NOT NULL
DROP TABLE Localization.Dictionary
END
BEGIN
	IF OBJECT_ID('Localization.Language', 'U') IS NOT NULL
DROP TABLE Localization.Language
END

--Create Languages table
CREATE TABLE Localization.Language(
	LanguageId VARCHAR(4),
	EnglishName VARCHAR(25) NOT NULL,
	LocalName VARCHAR(25),
	CONSTRAINT PK_Localization_Language PRIMARY KEY NONCLUSTERED (LanguageId)	
)

--Create Dictionary Table
CREATE TABLE Localization.Dictionary(
	EntryKey VARCHAR(50),
	LanguageId VARCHAR(4),
	Value VARCHAR(MAX),
	CONSTRAINT PK_LocalizationDictionary PRIMARY KEY NONCLUSTERED (EntryKey, LanguageId),
	CONSTRAINT FK_Dictionary_Language FOREIGN KEY (LanguageId)
		REFERENCES Localization.Language(LanguageId) ON DELETE CASCADE ON UPDATE CASCADE
);