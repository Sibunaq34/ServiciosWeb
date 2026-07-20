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

DROP PROCEDURE IF EXISTS sp_ObtenerRequisitosPorPuesto$$
CREATE PROCEDURE sp_ObtenerRequisitosPorPuesto(IN pCodigoPuesto VARCHAR(100))
BEGIN
    SELECT
        rp.id_requisito AS IdRequisito,
        rp.nombre_requisito AS NombreRequisito
    FROM puestos p
    INNER JOIN requisitos_puesto rp ON rp.id_puesto = p.id_puesto
    WHERE p.codigo_puesto = pCodigoPuesto
      AND p.activo = 1
      AND rp.activo = 1
    ORDER BY rp.nombre_requisito;
END$$

DROP PROCEDURE IF EXISTS sp_ObtenerOferentesPorPuesto$$
CREATE PROCEDURE sp_ObtenerOferentesPorPuesto(IN pCodigoPuesto VARCHAR(100))
BEGIN
    DECLARE vIdPuesto INT DEFAULT NULL;
    DECLARE vTotalRequisitos INT DEFAULT 0;

    SELECT id_puesto INTO vIdPuesto
    FROM puestos
    WHERE codigo_puesto = pCodigoPuesto
      AND activo = 1
    LIMIT 1;

    IF vIdPuesto IS NULL THEN
        SELECT
            NULL AS IdOferente,
            NULL AS NombreCompleto,
            NULL AS Identificacion
        WHERE 1 = 0;
    ELSE
        SELECT COUNT(*) INTO vTotalRequisitos
        FROM requisitos_puesto
        WHERE id_puesto = vIdPuesto
          AND activo = 1;

        IF vTotalRequisitos = 0 THEN
            SELECT
                o.id_oferente AS IdOferente,
                p.nombre_comple AS NombreCompleto,
                p.identificacion AS Identificacion
            FROM oferentes o
            INNER JOIN personas p ON p.id_persona = o.id_persona
            ORDER BY p.nombre_comple;
        ELSE
            SELECT
                o.id_oferente AS IdOferente,
                p.nombre_comple AS NombreCompleto,
                p.identificacion AS Identificacion
            FROM oferentes o
            INNER JOIN personas p ON p.id_persona = o.id_persona
            INNER JOIN oferente_requisito orq ON orq.id_oferente = o.id_oferente
            INNER JOIN requisitos_puesto rp ON rp.id_requisito = orq.id_requisito
            WHERE rp.id_puesto = vIdPuesto
              AND rp.activo = 1
            GROUP BY o.id_oferente, p.nombre_comple, p.identificacion
            HAVING COUNT(DISTINCT rp.id_requisito) = vTotalRequisitos
            ORDER BY p.nombre_comple;
        END IF;
    END IF;
END$$

DELIMITER ;
