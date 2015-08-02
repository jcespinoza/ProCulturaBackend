BEGIN TRAN

INSERT
    Localization.Dictionary
    (
        LanguageId,
        EntryKey,
        Value
    )
VALUES

('en', 'message_LoginSuccess', 'Login Successful!'),
('en', 'message_PasswordMismatch', 'Passwords do not match!'),
('en', 'message_EmailInUse', 'Email is already in use!'),
('en', 'message_EmailIsRequired', 'Email is required!'),
('en', 'message_RegistrationSuccess', 'Registration Successful!'),
('en', 'message_AuthRequestNotRecognized', 'Couldn''t validate permissions. Please log in again.'),
('en', 'message_InsufficientPrivileges', 'You do not have sufficient privileges to perform this action.'),
('en', 'message_UserDeleted', 'User successfully deleted!'),
('en', 'message_UserNotFound', 'User not found!'),
('en', 'message_UpdateUserSuccess', 'User successfully updated!'),
('en', 'message_InvalidPassword', 'Invalid Password.'),

('es', 'message_LoginSuccess','Autenticacion Exitosa!'),
('es', 'message_PasswordMismatch', 'La contraseña no coincide.'),
('es', 'message_EmailInUse','Este correo se encuentra en uso.'),
('es', 'message_EmailIsRequired', 'El correo electrónico es requerido!'),
('es', 'message_RegistrationSuccess', 'Registracion exitosa!'),
('es', 'message_AuthRequestNotRecognized', 'No se pudieron validar los permisos. Por favor realize la autenticacion nuevamente.'),
('es', 'message_InsufficientPrivileges', 'No tiene suficientes privilegios para realizar esta operacion.'),
('es', 'message_UserDeleted', 'Usuario borrado exitosamente!'),
('es', 'message_UserNotFound', 'No se pudo encontrar el usuario!'),
('es', 'message_UpdateUserSuccess', 'Usuario actualizado exitosamente!'),
('es', 'message_InvalidPassword', 'Contra contraseña es invalida.'),

('fr', 'message_LoginSuccess', 'Connexion Réussie!'),
('fr', 'message_PasswordMismatch', 'Les mots de passe ne correspondent pas!'),
('fr', 'message_EmailInUse', 'Cet email est déjà utilisé!'),
('fr', 'message_RegistrationSuccess', 'Inscription Réussi!'),

('de', 'message_LoginSuccess', 'Anmeldung erfolgreich!'),
('de', 'message_PasswordMismatch', 'Passwoerter stimmen nicht ueberein!'),
('de', 'message_EmailInUse', 'E-mail ist bereits Einsatz!'),
('de', 'message_RegistrationSuccess', 'Registrierung erfolgreich!')

COMMIT
--ROLLBACK