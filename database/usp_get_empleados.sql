SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.usp_get_empleados') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.usp_get_empleados
GO

CREATE PROCEDURE usp_get_empleados
	@ID INT
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

SELECT ClaveEmpleado
, NombreEmpleado
, FechaIngreso
, FechaNacimiento
, ClaveDepartamento
FROM Empleados
WHERE (@ID IS NOT NULL AND ClaveEmpleado = @ID)
OR @ID IS NULL 
