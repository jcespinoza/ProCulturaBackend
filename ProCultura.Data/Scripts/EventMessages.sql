BEGIN TRAN

INSERT
Localization.Dictionary
VALUES
	('message_DuplicateResource', 'en', 'This resource already exists'),
	('message_EventNotFound', 'en', 'The event could not be found'),
	('message_EventAlreadyExist', 'en', 'This Event already exist'),

	('message_DuplicateResource', 'es', 'Este recurso ya existe'),
	('message_EventNotFound', 'es', 'No se pudo encontrar el evento'),
	('message_EventAlreadyExist', 'es', 'Este evento ya existe')

COMMIT
--ROLLBACK