SET NOCOUNT ON
USE dev
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE ID = OBJECT_ID('dbo.usp_ins_empleados') AND SYSSTAT & 0xf = 4)
	DROP PROCEDURE dbo.usp_ins_empleados
GO

CREATE PROCEDURE usp_ins_empleados
	@ClaveEmpleado INT = NULL,
	@NombreEmpleado VARCHAR(MAX) = NULL,
	@FechaIngreso DATE = NULL,
	@FechaNacimiento DATE = NULL,
	@ClaveDepartamento INT = NULL
AS 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

DECLARE @val INT = NULL
DECLARE @NEWEMP INT = 0

IF @NombreEmpleado IS NULL BEGIN RETURN @NEWEMP END 
IF @FechaIngreso IS NULL BEGIN RETURN @NEWEMP END 
IF @FechaNacimiento IS NULL BEGIN RETURN @NEWEMP END 
IF @ClaveDepartamento IS NULL BEGIN RETURN @NEWEMP END 

-- Se valida si existe la claveEmpleado
IF EXISTS (SELECT TOP 1 * FROM Empleados WHERE ClaveEmpleado = @ClaveEmpleado)
BEGIN 
	SET @val = 1
END 

IF @val IS NULL 
BEGIN 
	INSERT INTO Empleados (ClaveEmpleado, NombreEmpleado, FechaIngreso, FechaNacimiento, ClaveDepartamento)
	VALUES (@ClaveEmpleado, @NombreEmpleado, @FechaIngreso, @FechaNacimiento, @ClaveDepartamento)
	
	SET @NEWEMP = SCOPE_IDENTITY()

	RETURN @NEWEMP
END 

IF @val IS NOT NULL 
BEGIN 
	UPDATE Empleados 
		SET 
			NombreEmpleado = @NombreEmpleado,
			FechaIngreso = @FechaIngreso,
			ClaveDepartamento = @ClaveDepartamento
		WHERE ClaveEmpleado = @ClaveEmpleado
	
	RETURN @ClaveEmpleado
END 