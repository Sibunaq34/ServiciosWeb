DELIMITER $$

DROP PROCEDURE IF EXISTS ValidarUsuario$$
CREATE PROCEDURE ValidarUsuario(IN pUsuario VARCHAR(100))
BEGIN
    SELECT
        u.id_usuario AS IdUsuario,
        u.usuario AS Usuario,
        u.nombre_completo AS NombreCompleto,
        u.contrasena AS PasswordCifrada,
        u.activo AS Activo,
        u.estado AS Estado,
        COALESCE(u.intentos_fallidos, 0) AS IntentosFallidos,
        r.id_rol AS IdRol,
        r.nombre_permiso AS NombreRol
    FROM Usuarios u
    LEFT JOIN UsuarioRol ur ON ur.id_usuario = u.id_usuario
    LEFT JOIN Roles r ON r.id_rol = ur.id_rol
    WHERE LOWER(u.usuario) = LOWER(pUsuario)
    LIMIT 1;
END$$

DROP PROCEDURE IF EXISTS RegistrarIntentoFallido$$
CREATE PROCEDURE RegistrarIntentoFallido(IN pIdUsuario INT, IN pIntentos INT)
BEGIN
    UPDATE Usuarios
    SET intentos_fallidos = pIntentos,
        estado = CASE WHEN pIntentos >= 3 THEN 'Bloqueado' ELSE estado END,
        activo = CASE WHEN pIntentos >= 3 THEN 0 ELSE activo END
    WHERE id_usuario = pIdUsuario;
END$$

DROP PROCEDURE IF EXISTS ReiniciarIntentosFallidos$$
CREATE PROCEDURE ReiniciarIntentosFallidos(IN pIdUsuario INT)
BEGIN
    UPDATE Usuarios
    SET intentos_fallidos = 0
    WHERE id_usuario = pIdUsuario;
END$$

DROP PROCEDURE IF EXISTS ActualizarPasswordCifradaUsuario$$
CREATE PROCEDURE ActualizarPasswordCifradaUsuario(IN pIdUsuario INT, IN pPasswordCifrada VARCHAR(500))
BEGIN
    UPDATE Usuarios
    SET contrasena = pPasswordCifrada
    WHERE id_usuario = pIdUsuario;
END$$

DELIMITER ;
