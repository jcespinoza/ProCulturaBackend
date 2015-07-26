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
		'Español'
	),
	(
		'fr',
		'French',
		'Français'
	),
	(
		'de',
		'German',
		'Deutsch'
	)

COMMIT
--ROLLBACK