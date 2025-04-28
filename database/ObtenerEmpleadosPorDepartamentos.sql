SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.ObtenerEmpleadosPorDepartamentos') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.ObtenerEmpleadosPorDepartamentos
GO
CREATE PROCEDURE ObtenerEmpleadosPorDepartamentos    
AS
BEGIN
    SELECT 
        E.ClaveEmpleado,
        E.NombreEmpleado,
        (VAR)E.FechaIngreso,
        S.SueldoMensual,
		D.Descripcion
    FROM Empleados E
    INNER JOIN Sueldos S ON E.ClaveEmpleado = S.ClaveEmpleado    
	INNER JOIN Departamentos D On E.ClaveDepartamento = D.ClaveDepartamento
    ORDER BY E.ClaveEmpleado;
END;