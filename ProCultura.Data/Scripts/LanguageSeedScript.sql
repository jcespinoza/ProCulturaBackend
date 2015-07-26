USE ProCulturaBackendDB
BEGIN TRAN

INSERT
	Localization.Language
	(
		LanguageId,
		EnglishName,
		LocalName
	)
VALUES
	(
		'en',
		'English',
		'English'
	),
	(
		'es',
		'Spanish',
		'Espa�ol'
	),
	(
		'fr',
		'French',
		'Fran�ais'
	),
	(
		'de',
		'German',
		'Deutsch'
	)

COMMIT
--ROLLBACK